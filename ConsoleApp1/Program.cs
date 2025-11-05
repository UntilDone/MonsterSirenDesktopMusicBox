using NLua;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class Program
{
    static void Main()
    {
        Console.WriteLine(RuntimeInformation.OSDescription);
        Console.WriteLine(RuntimeInformation.FrameworkDescription);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        Console.WriteLine("Running on macOS (또는 iOS 시뮬레이터)");
        Console.WriteLine("=== C# + NLua Test ===");

        using var lua = new Lua();

        string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts", "enemy_ai.lua");

        // 최초 로드
        LoadAndRunLua(lua, scriptPath, 80); // Player HP = 80
        LoadAndRunLua(lua, scriptPath, 30); // Player HP = 30

        Console.WriteLine("\n이제 Lua 파일을 수정하고 엔터를 눌러보세요.");
        Console.ReadLine();

        // Lua 파일 수정 후 다시 실행
        LoadAndRunLua(lua, scriptPath, 30);
    }

    static void LoadAndRunLua(Lua lua, string path, int playerHp)
    {
        lua.DoFile(path); // 스크립트 다시 읽기
        var func = lua["EnemyAttack"] as LuaFunction;

        Console.WriteLine($"[C#] Calling Lua: EnemyAttack({playerHp})");
        func?.Call(playerHp);
    }

}