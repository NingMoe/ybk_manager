using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
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

namespace YbkManage.Activities
{
    [Activity(Label = "AssistantAddActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AssistantAddActivity : AppActivity
    {
        // 标题
        private TextView tvTitle;
        // 添加、删除按钮
        private TextView tvSave;
        private Button btnAdd, btnDelete;

        private EditText etName, etAmount;
        private RelativeLayout rlArea;
        private TextView tvArea;

        private bool isNewAdd = true;
        private AstLeaderListModel currAssistant = new AstLeaderListModel();

        private MeService _meService = new MeService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_assistant_add;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            Bundle bundle = Intent.Extras;
            if (bundle != null)
            {
                var assistantJsonStr = bundle.GetString("assistantJsonStr");
                if (!string.IsNullOrEmpty(assistantJsonStr))
                {
                    currAssistant = JsonSerializer.ToObject<AstLeaderListModel>(assistantJsonStr);
                }
            }
        }

        protected override void InitViews()
        {
            tvTitle = FindViewById<TextView>(Resource.Id.tv_title);

            etName = FindViewById<EditText>(Resource.Id.et_name);
            etAmount = FindViewById<EditText>(Resource.Id.et_amount);

            tvSave = FindViewById<TextView>(Resource.Id.tv_save);
            btnAdd = FindViewById<Button>(Resource.Id.btn_add);
            btnDelete = FindViewById<Button>(Resource.Id.btn_delete);

            rlArea = (RelativeLayout)FindViewById(Resource.Id.rl_area);
            tvArea = FindViewById<TextView>(Resource.Id.tv_area);

            // 添加教师情况
            if (currAssistant == null || string.IsNullOrEmpty(currAssistant.Name))
            {
                tvTitle.Text = "添加助教组长";

                btnAdd.Visibility = ViewStates.Visible;
                btnDelete.Visibility = ViewStates.Gone;
            }
            else
            {
                isNewAdd = false;
                tvTitle.Text = "编辑助88888教组长";

                tvTitle.Text = currAssistant.Name+"fdsfa";
                btnAdd.Visibility = ViewStates.Gone;
                btnDelete.Visibility = ViewStates.Visible;

				etName.Enabled = false;
				etAmount.Enabled = false;
                etName.Text = currAssistant.Name;
                etAmount.Text = currAssistant.Mobile;
                tvArea.Text = currAssistant.AreaName;
                tvArea.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
            }
        }

        protected override void InitEvents()
        {
            // 取消
            FindViewById<TextView>(Resource.Id.tv_cancel).Click += (sender, e) =>
           {
               CurrActivity.Finish();
               OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
           };

            // 选择学区
            rlArea.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(AreaSelectActivity));
                intent.PutExtra("sname", currAssistant.AreaName);
                StartActivityForResult(intent, 0);
                CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            };

            btnAdd.Click += (sender, e) =>
            {
                DoSave(true);
            };

            tvSave.Click += (sender, e) =>
            {
                if (isNewAdd)
                {
                    DoSave(false);
                }
                else
                {
                    DoUpdate();
                }
            };

            btnDelete.Click += (sender, e) =>
            {
                var callbackFunc = new AppUtils.ShowDialogClick(CallbackFun);
                AppUtils.ShowDialog(CurrActivity, "提示", "您确认要删除此账号吗？", 2, callbackFunc);
            };
        }

        private void CallbackFun(int type)
        {
            if (type == 1)
            {
                DoDelete();
            }
        }

        /// <summary>
        /// 保存教师信息
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
                currAssistant.Name = etName.Text.Trim();
                currAssistant.Mobile = etAmount.Text.Trim();
                if (string.IsNullOrEmpty(currAssistant.Name))
                {
                    ToastUtil.ShowWarningToast(this, "请输入姓名");
                    etName.RequestFocus();
                    return;
                }

				if (!Helper.IsMobile(currAssistant.Mobile))
                {
                    ToastUtil.ShowWarningToast(this, "请输入正确的手机号");
                    etAmount.RequestFocus();
                    return;
                }
                if (string.IsNullOrEmpty(currAssistant.AreaCode))
                {
                    ToastUtil.ShowWarningToast(this, "请选择教学区");
                    etAmount.RequestFocus();
                    return;
                }

                LoadingDialogUtil.ShowLoadingDialog(this, "保存中...");

                new Thread(new ThreadStart(() =>
                            {
                                //新增操作
                                var model = new ManagerUserInfo();
                                model.Mobile = currAssistant.Mobile;
                                model.Name = currAssistant.Name;
                                model.IsCanLogin = false;
                                model.UserType = (int)UserType.AssistantLeader;
                                model.SchoolId = CurrUserInfo.SchoolId;
                                model.Creator = CurrUserInfo.Name;
                                model.Modifier = CurrUserInfo.Name;
                                var resultData = _meService.AddManagerUser(model, currAssistant.AreaCode, currAssistant.AreaName, 0);

                                RunOnUiThread(() =>
                                {
                                    LoadingDialogUtil.DismissLoadingDialog();
                                    if (resultData.State == 1)
                                    {
                                        ToastUtil.ShowSuccessToast(this, "操作成功");
                                        //保存并继续添加爱
                                        if (isContinueAdd)
                                        {
                                            currAssistant = new AstLeaderListModel();
                                            etName.Text = "";
                                            etAmount.Text = "";
                                            tvArea.Text = "未设置";
                                            tvArea.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
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
            }
            finally
            {
                LoadingDialogUtil.DismissLoadingDialog();
            }
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        private void DoUpdate()
        {
            try
            {
                if (!NetUtil.CheckNetWork(CurrActivity))
                {
                    ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
                    return;
                }


                LoadingDialogUtil.ShowLoadingDialog(this, "提交中...");

                new Thread(new ThreadStart(() =>
                            {

                                var relation = new UserAreaRelationModel();
                                relation.AreaCode = currAssistant.AreaCode;
                                relation.AreaName = currAssistant.AreaName;
                                relation.AssistantMobile = currAssistant.Mobile;
                                relation.Creator = CurrUserInfo.Name;
                                relation.Modifier = CurrUserInfo.Name;
                                relation.SchoolId = CurrUserInfo.SchoolId;
                                var list = new List<UserAreaRelationModel>();
                                list.Add(relation);
                                var rd = _meService.SaveUserArea(list);

                                RunOnUiThread(() =>
                                {
                                    LoadingDialogUtil.DismissLoadingDialog();
                                    if (rd.State == 1)
                                    {
                                        ToastUtil.ShowSuccessToast(this, "操作成功");
                                        new Handler().PostDelayed(() =>
                                            {

                                                Finish();
                                                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
                                            }, 1000);
                                    }
                                    else
                                    {
                                        ToastUtil.ShowErrorToast(this, (string.IsNullOrEmpty(rd.Error) ? "操作失败" : rd.Error));
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

		/// <summary>
		/// 删除教师信息
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
								var type = 1; //type = 1 助教相关身份 type = 2 教师相关身份
								var keyword = currAssistant.Mobile;
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


        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Android.App.Result.Ok)
            {
                if (requestCode == 0)
                {
                    currAssistant.AreaName = data.GetStringExtra("sname");
                    tvArea.Text = data.GetStringExtra("sname");
                    currAssistant.AreaCode = data.GetStringExtra("scode");
                    tvArea.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                }
            }
        }
    }
}