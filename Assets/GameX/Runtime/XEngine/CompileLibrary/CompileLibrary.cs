using System;
using System.Collections.Generic;
using System.Reflection;

public class CompileLibrary
{
    /// <summary>
    /// 加载类库（.dll）路径
    /// </summary>
    private string path;

    /// <summary>
    /// 程序集实例
    /// </summary>
    private Assembly assembly;

    /// <summary>
    /// 接收实例数组
    /// </summary>
    private Type[] typeArr;


    /// <summary>
    /// 实例类型列表
    /// </summary>
    private List<Type> types;

    /// <summary>
    /// 实例对象
    /// </summary>
    public object instance;

    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="libPath">后缀(.dll)需要保留</param>
    /// <returns></returns>
    public List<Type> LoadAssembly(string libPath)
    {
        if(string.IsNullOrEmpty(libPath))
            return null;

        path = libPath;

        path = path.Replace('\\', '/');

        assembly = Assembly.LoadFrom(path);

        // 1. 加载DLL
        if (assembly == null)
        {
            Debug.LogWarning("Assembly library is not loaded.");

            return null;
        }

        // 2. 获取类型
        typeArr = assembly.GetTypes();

        // 3. 构建类型列表
        types = new List<Type>(typeArr);

        // 4. 返回类型列表
        return types;
    }

    /// <summary>
    /// 创建类实例
    /// </summary>
    /// <param name="type_name">程序集中所包含的类文件名</param>
    /// <returns></returns>
    public object CreateType(string type_name)
    {
        if (string.IsNullOrEmpty(type_name))
            return null;

        for (int i = 0; i < types.Count; i++)
        {
            //创建实例
            object instance = Activator.CreateInstance(types[i]);

            //将类型实例注册设置到 xLua.Global 元表中
            Lua.Set<string, object>(instance.GetType().Name , instance);

            if (types[i].Name == type_name)
            {
                return types[i];
            }
        }

        return null;
    }
}