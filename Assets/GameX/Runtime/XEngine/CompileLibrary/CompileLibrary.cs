using System;
using System.Collections.Generic;
using System.Reflection;

public class CompileLibrary
{
    /// <summary>
    /// ������⣨.dll��·��
    /// </summary>
    private string path;

    /// <summary>
    /// ����ʵ��
    /// </summary>
    private Assembly assembly;

    /// <summary>
    /// ����ʵ������
    /// </summary>
    private Type[] typeArr;


    /// <summary>
    /// ʵ�������б�
    /// </summary>
    private List<Type> types;

    /// <summary>
    /// ʵ������
    /// </summary>
    public object instance;

    /// <summary>
    /// ���س���
    /// </summary>
    /// <param name="libPath">��׺(.dll)��Ҫ����</param>
    /// <returns></returns>
    public List<Type> LoadAssembly(string libPath)
    {
        if(string.IsNullOrEmpty(libPath))
            return null;

        path = libPath;

        path = path.Replace('\\', '/');

        assembly = Assembly.LoadFrom(path);

        // 1. ����DLL
        if (assembly == null)
        {
            Debug.LogWarning("Assembly library is not loaded.");

            return null;
        }

        // 2. ��ȡ����
        typeArr = assembly.GetTypes();

        // 3. ���������б�
        types = new List<Type>(typeArr);

        // 4. ���������б�
        return types;
    }

    /// <summary>
    /// ������ʵ��
    /// </summary>
    /// <param name="type_name">�����������������ļ���</param>
    /// <returns></returns>
    public object CreateType(string type_name)
    {
        if (string.IsNullOrEmpty(type_name))
            return null;

        for (int i = 0; i < types.Count; i++)
        {
            //����ʵ��
            object instance = Activator.CreateInstance(types[i]);

            //������ʵ��ע�����õ� xLua.Global Ԫ����
            Lua.Set<string, object>(instance.GetType().Name , instance);

            if (types[i].Name == type_name)
            {
                return types[i];
            }
        }

        return null;
    }
}