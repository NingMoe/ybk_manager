using System;
using Android.Content;

namespace xxxxxLibrary.Utils
{
    /// <summary>
    /// 读取sp的工具类
    /// 2017-07-17
    /// </summary>
    public class SharedPreferencesUtil
    {
        /// <summary>
        /// 保存在手机里面的文件名 
        /// </summary>
        private const String FILE_NAME = "sp_xdf";


        /// <summary>
        /// 保存数据的方法，我们需要拿到保存数据的具体类型，然后根据类型调用不同的保存方法 
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="key">Key.</param>
        /// <param name="obj">Object.</param>
        public static void SetParam(Context context, String key, Object obj)
        {
            String type = obj.GetType().Name;
            ISharedPreferences sp = context.GetSharedPreferences(FILE_NAME, FileCreationMode.Private);
            ISharedPreferencesEditor editor = sp.Edit();

            if ("String".Equals(type))
            {
                editor.PutString(key, (String)obj);
            }
            else if ("Integer".Equals(type))
            {
                editor.PutInt(key, Convert.ToInt32(obj));
            }
            else if ("Boolean".Equals(type))
            {
                editor.PutBoolean(key, (Boolean)obj);
            }
            else if ("Float".Equals(type))
            {
                editor.PutLong(key, long.Parse(obj.ToString()));
            }
            else if ("Long".Equals(type))
            {
                editor.PutLong(key, Convert.ToInt64(obj));
            }

            editor.Commit();
        }

        /// <summary>
        /// 得到保存数据的方法，我们根据默认值得到保存的数据的具体类型，然后调用相对于的方法获取值 
        /// </summary>
        /// <returns>The parameter.</returns>
        /// <param name="context">Context.</param>
        /// <param name="key">Key.</param>
        /// <param name="defaultObject">Default object.</param>
        public static Object GetParam(Context context, String key, Object defaultObject)
        {
            String type = defaultObject.GetType().Name;
            ISharedPreferences sp = context.GetSharedPreferences(FILE_NAME, FileCreationMode.Private);

            if ("String".Equals(type))
            {
                return sp.GetString(key, (String)defaultObject);
            }
            else if ("Integer".Equals(type))
            {
                return sp.GetInt(key, Int32.Parse(defaultObject.ToString()));
            }
            else if ("Boolean".Equals(type))
            {
                return sp.GetBoolean(key, bool.Parse(defaultObject.ToString()));
            }
            else if ("Float".Equals(type))
            {
                return sp.GetFloat(key, float.Parse(defaultObject.ToString()));
            }
            else if ("Long".Equals(type))
            {
                return sp.GetLong(key, long.Parse(defaultObject.ToString()));
            }

            return null;
        }

        /// <summary>
        /// 删除数据的方法
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="key">Key.</param>
        public static void DeleteParam(Context context, String key)
        {

            ISharedPreferences sp = context.GetSharedPreferences(FILE_NAME, FileCreationMode.Private);
            ISharedPreferencesEditor editor = sp.Edit();
            editor.Remove(key);
            editor.Commit();
        }
    }
}
