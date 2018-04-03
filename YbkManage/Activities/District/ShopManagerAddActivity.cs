using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using xxxxxLibrary.Utils;
using YbkManage.App;
using System.Linq;
using YbkManage.Activities;
using System.Collections.Generic;

namespace YbkManage
{
	[Activity(Label = "ShopManagerAddActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ShopManagerAddActivity : AppActivity
	{
		#region UIField
		// 返回按钮
		private ImageButton imgbtnBack;
		private RelativeLayout rlAreas;
		// 标题
		private TextView tvTitle;
		// 添加、删除按钮
		private TextView tvSave;
		private Button btnAdd, btnDelete;
		private TextView tvAreaNames,tvAreaCodes;
		private EditText etName, etEmail;



		#endregion

		#region Field
		private MeService _meService = new MeService();

		private bool isNewAdd = true;
		private ShopManagerList currShopManager = new ShopManagerList();
		private string AreaCodes { get; set; }
		private string AreaNames { get; set; }

		#endregion

		protected override void OnCreate(Bundle savedInstanceState)
		{
			LayoutReourceId = Resource.Layout.activity_shopmanager_add;

			base.OnCreate(savedInstanceState);
		}

		/// <summary>
		/// 处理上级页面传过来的参数
		/// </summary>
		protected override void InitVariables()
		{
			Bundle bundle = Intent.Extras;
			if (bundle != null)
			{
				
				var jsonStr = bundle.GetString("ShopManagerJsonStr");
				if (!string.IsNullOrEmpty(jsonStr))
				{
					currShopManager = JsonSerializer.ToObject<ShopManagerList>(jsonStr);
				}

				AreaCodes = bundle.GetString("areaCodes");
				AreaNames = bundle.GetString("areaNames");
			}
		}

		/// <summary>
		/// 初始化页面上的控件
		/// </summary>
		protected override void InitViews()
		{
			imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
			rlAreas = (RelativeLayout)FindViewById(Resource.Id.rl_areas);
			etName = FindViewById<EditText>(Resource.Id.et_name);
			etEmail = FindViewById<EditText>(Resource.Id.et_email);
			tvTitle = FindViewById<TextView>(Resource.Id.tv_title);
			tvSave = FindViewById<TextView>(Resource.Id.tv_save);
			btnAdd = FindViewById<Button>(Resource.Id.btn_add);
			btnDelete = FindViewById<Button>(Resource.Id.btn_delete);
			tvAreaNames = FindViewById<TextView>(Resource.Id.tv_areaNames);
			tvAreaCodes = FindViewById<TextView>(Resource.Id.tv_areaCodes);

			// 添加
			if (currShopManager == null || string.IsNullOrEmpty(currShopManager.Name))
			{
				btnAdd.Visibility = ViewStates.Visible;
				btnDelete.Visibility = ViewStates.Gone;
				tvAreaNames.Text = AreaNames??"未设置";
				tvAreaCodes.Text = AreaCodes??"";
				currShopManager = new ShopManagerList();
			}
			else
			{
				isNewAdd = false;
				tvTitle.Text = currShopManager.Name;
				btnAdd.Visibility = ViewStates.Gone;
				btnDelete.Visibility = ViewStates.Visible;

				etName.Enabled = false;
				etEmail.Enabled = false;
				etName.Text = currShopManager.Name;
				etEmail.Text = currShopManager.Email;

				tvAreaNames.Text = AreaNames;
				tvAreaCodes.Text = AreaCodes;

			}
		}

		/// <summary>
		/// 初始化事件
		/// </summary>
		protected override void InitEvents()
		{
			// 返回
			imgbtnBack.Click += (sender, e) =>
			{
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};

			// 选择负责校区
			rlAreas.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(AreaSelectMultiActivity));
				if (currShopManager != null)
				{
					intent.PutExtra("areaCodes", AreaCodes);
				}
				StartActivityForResult(intent, 1);
				CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);

			};
			//保存并继续添加
			btnAdd.Click += (sender, e) =>
			{
				DoSave(true);
			};
			//完成
			tvSave.Click += (sender, e) =>
			{
				DoSave(false);
			};
			//删除
			btnDelete.Click += (sender, e) =>
			{
				var callbackFunc = new AppUtils.ShowDialogClick(CallbackFun);
				AppUtils.ShowDialog(CurrActivity, "提示", "您确认要删除此信息吗？", 2, callbackFunc);
			};
		}



		#region 添加／编辑店长信息

		/// <summary>
		/// 添加／编辑店长信息
		/// </summary>
		/// <param name="isContinueAdd">isContinueAdd=true：连续保存</param>
		private void DoSave(bool isContinueAdd)
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}
			try
			{
				var tname = etName.Text.Trim();
				var temail = etEmail.Text.Trim();

				if (string.IsNullOrEmpty(tname))
				{
					ToastUtil.ShowWarningToast(this, "请输入姓名");
					etName.RequestFocus();
					return;
				}
				if (!CheckUtil.IsValidEmail(temail))
				{
					ToastUtil.ShowWarningToast(this, "邮箱格式不正确");
					etEmail.RequestFocus();
					return;
				}

				if (string.IsNullOrEmpty(tvAreaCodes.Text.Trim()))
				{
					ToastUtil.ShowWarningToast(this, "请选择门店");
					return;
				}



				LoadingDialogUtil.ShowLoadingDialog(this, "保存中...");

				new Thread(new ThreadStart(() =>
							{
								//新增操作
								var model = new ManagerUserInfo();
								model.Email = etEmail.Text;
								model.Name = etName.Text;
								model.UserType = Convert.ToInt32(UserType.ShopManager);
								model.IsCanLogin = true;
								model.SchoolId = CurrUserInfo.SchoolId;
								model.Creator = CurrUserInfo.Name;
								model.Modifier = CurrUserInfo.Name;
								model.DistrictCode = CurrUserInfo.DistrictCode;




								DataEntity.Result resultData = new DataEntity.Result();

								if (isNewAdd)
								{
									resultData = _meService.AddShopManager(model, AreaCodes, AreaNames);
								}
								else
								{
									var codeArr = AreaCodes.Split(',');
									var nameArr = AreaNames.Split(',');
									var list = new List<UserAreaRelationModel>();
									for (int i = 0; i < codeArr.Length; i++)
									{
										var relation = new UserAreaRelationModel();
										relation.AreaCode = codeArr[i];
										relation.AreaName = nameArr[i];
										relation.Email = etEmail.Text;
										relation.UserType = (int)UserType.ShopManager;
										relation.Creator = CurrUserInfo.Name;
										relation.Modifier = CurrUserInfo.Name;
										relation.SchoolId = CurrUserInfo.SchoolId;
										list.Add(relation);
									}
									resultData = _meService.SaveUserArea(list);

								}

								RunOnUiThread(() =>
								{
									LoadingDialogUtil.DismissLoadingDialog();
									if (resultData.State == 1)
									{
										ToastUtil.ShowSuccessToast(this, "操作成功");
										//保存并继续添加
										if (isContinueAdd)
										{
											currShopManager = new ShopManagerList();
											etName.Text = "";
											etEmail.Text = "";
											tvAreaNames.Text = "未设置";
										}
										//完成
										else
										{
											new Handler().PostDelayed(() =>
												{
													Finish();
													OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
												}, 1000);
										}
									}
									else
									{
										ToastUtil.ShowErrorToast(this, (string.IsNullOrEmpty(resultData.Error) ? "操作失败" : resultData.Error));
									}

								});

							})).Start();

			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				ToastUtil.ShowErrorToast(this, "操作失败");
				LoadingDialogUtil.DismissLoadingDialog();
			}
		}
		#endregion

		#region 删除
		private void CallbackFun(int type)
		{
			if (type == 1)
			{
				DoDelete();
			}
		}

		/// <summary>
		/// 删除店长
		/// </summary>
		private void DoDelete()
		{
			try
			{
				if (!NetUtil.CheckNetWork(CurrActivity))
				{
					ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
					return;
				}


				LoadingDialogUtil.ShowLoadingDialog(this, "删除中...");

				new Thread(new ThreadStart(() =>
							{
								var schoolId = CurrUserInfo.SchoolId;
								var type = 3; //type = 1 助教相关身份 type = 2 教师相关身份 type=3 区域相关身份

								var keyword = etEmail.Text;
								var modifier = CurrUserInfo.Name;
								var rd = _meService.DeleteManagerUser(schoolId, type.ToString(), keyword, modifier);


								RunOnUiThread(() =>
								{
									LoadingDialogUtil.DismissLoadingDialog();
									if (rd.State == 0)
									{
										ToastUtil.ShowErrorToast(this, (string.IsNullOrEmpty(rd.Error) ? "操作失败" : rd.Error));
									}
									else
									{
										if (BaseApplication.GetInstance().teacherList != null)
										{
											//BaseApplication.GetInstance().teacherList.Remove(currTeacher);
										}
										ToastUtil.ShowSuccessToast(this, "操作成功");
										new Handler().PostDelayed(() =>
											{

												Finish();
												OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
											}, 1000);
									}


								});

							})).Start();

			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				ToastUtil.ShowErrorToast(this, "操作失败");
			}
			finally
			{
				LoadingDialogUtil.DismissLoadingDialog();
			}
		}
		#endregion

		/// <summary>
		/// 选择门店后，返回该页面，会执行此方法
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="resultCode">Result code.</param>
		/// <param name="data">Data.</param>
		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			//选择门店后，返回该页面，执行此方法
			if (resultCode == Android.App.Result.Ok)
			{
				AreaCodes= data.GetStringExtra("areaCodes");
				AreaNames = data.GetStringExtra("areaNames");
				tvAreaNames.Text = AreaNames;
				tvAreaCodes.Text = AreaCodes;

			}
		}
	}
}
