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
    /// 创建Lua实例
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
    /// 获取启动器
    /// </summary>
    /// <returns></returns>
    public static LuaEnv GetEnv()
    {
        return Lua.luaEnv;
    }


    /// <summary>
    /// 设置启动器
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
    /// 获取Global表
    /// </summary>
    /// <returns></returns>
    public static LuaTable GetGlobal()
    {
        LuaEnv lua = GetEnv();

        return lua.Global;
    }



    /// <summary>
    /// 获取Global表中元素
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
    /// 在Global表中添加元素
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
    /// 释放Lua代码
    /// </summary>
    public static void Dispose()
    {
        LuaEnv luaEnv = GetEnv();
        luaEnv.Dispose();
        Instance = null;
    }
}