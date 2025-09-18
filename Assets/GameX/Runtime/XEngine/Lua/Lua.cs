using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

public class Lua
{

    public static Lua Instance = null;

    private static LuaEnv luaEnv = null;

    public static Dictionary<string, string> LuaList = new Dictionary<string, string>();

    /// <summary>
    /// ����Luaʵ��
    /// </summary>
    /// <returns></returns>
    public static Lua Create()
    {
        if (Instance == null)
        {
            Instance = new Lua();

            if (luaEnv == null)
            {
                luaEnv = new LuaEnv();

                SetEnv(luaEnv);
            }
        }

        return Instance;
    }

    /// <summary>
    /// ��ȡ������
    /// </summary>
    /// <returns></returns>
    public static LuaEnv GetEnv()
    {
        return Lua.luaEnv;
    }


    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="luaEnv"></param>
    public static void SetEnv(LuaEnv _luaEnv)
    {
        if (_luaEnv != null)
        {
            luaEnv = _luaEnv;
        }
    }



    /// <summary>
    /// ��ȡGlobal��
    /// </summary>
    /// <returns></returns>
    public static LuaTable GetGlobal()
    {
        LuaEnv lua = GetEnv();

        return lua.Global;
    }



    /// <summary>
    /// ��ȡGlobal����Ԫ��
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Get<TKey, TValue>(TKey key, out TValue value)
    {
        LuaTable lua = GetGlobal();

        lua.Get(key, out value);
    }



    /// <summary>
    /// ��Global�������Ԫ��
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Set<TKey, TValue>(TKey key, TValue value)
    {
        if (LuaList.ContainsKey(key.ToString()))
            return;

        LuaTable lua = GetGlobal();

        lua.Set(key, value);

        LuaList.Add(key.ToString(), value.ToString());
    }


    /// <summary>
    /// �ͷ�Lua����
    /// </summary>
    public static void Dispose()
    {
        LuaEnv luaEnv = GetEnv();
        luaEnv.Dispose();
        Instance = null;
    }
}