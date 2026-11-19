using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using UnityEngine.InputSystem;

[BepInPlugin("br.moneyball.fm27", "Moneyball FM27", "1.0.0")]
public sealed class MoneyballPlugin : BasePlugin
{
    public override void Load()
    {
        AddComponent<MoneyballShortcut>();
        Log.LogInfo("Atalho do Moneyball: Ctrl+Shift+M.");
    }
}

public sealed class MoneyballShortcut : MonoBehaviour
{
    private const string Url = "http://127.0.0.1:27927/";
    private readonly string token = Guid.NewGuid().ToString("N");
    private HttpListener listener;

    public MoneyballShortcut(IntPtr pointer) : base(pointer) {}

    private void Update()
    {
        try
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard.ctrlKey.isPressed && keyboard.shiftKey.isPressed && keyboard.mKey.wasPressedThisFrame)
                Open();
        }
        catch (Exception error) { UnityEngine.Debug.LogError("Moneyball FM27: " + error.Message); }
    }

    private void Open()
    {
        if (listener == null)
        {
            var server = new HttpListener();
            server.Prefixes.Add(Url);
            try { server.Start(); } catch { server.Close(); throw; }
            listener = server;
            new Thread(() => Serve(server)) { IsBackground = true }.Start();
        }
        Process.Start(new ProcessStartInfo(Url + "?session=" + token) { UseShellExecute = true });
    }

    private void OnDestroy()
    {
        if (listener != null) listener.Close();
    }

    private void Serve(HttpListener server)
    {
        while (server.IsListening)
        {
            HttpListenerContext context;
            try { context = server.GetContext(); }
            catch (HttpListenerException) { return; }
            catch (ObjectDisposedException) { return; }
            try { Respond(context); }
            catch (IOException) { context.Response.StatusCode = 500; }
            catch (Exception error) { UnityEngine.Debug.LogError("Moneyball FM27: " + error.Message); }
            finally { context.Response.Close(); }
        }
    }

    private void Respond(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;
        response.Headers["Cache-Control"] = "no-store";
        response.Headers["X-Content-Type-Options"] = "nosniff";
        response.Headers["Referrer-Policy"] = "no-referrer";
        if (request.HttpMethod != "GET" || request.QueryString["session"] != token)
        {
            response.StatusCode = 403;
            return;
        }
        if (request.Url.AbsolutePath == "/")
        {
            var folder = Path.GetDirectoryName(GetType().Assembly.Location);
            Write(response, "text/html", File.ReadAllText(Path.Combine(folder, "index.html")));
            return;
        }
        if (request.Url.AbsolutePath != "/latest") { response.StatusCode = 404; return; }
        var docs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sports Interactive");
        var fm = Path.Combine(docs, "Football Manager 27");
        var dirs = new[] { Path.Combine(fm, "exports"), Path.Combine(fm, "FM27PlayerExport by vinteset"), Path.Combine(docs, "FM View Exporter") };
        FileInfo latest = null;
        foreach (var dir in dirs)
        {
            if (!Directory.Exists(dir)) continue;
            foreach (var path in Directory.EnumerateFiles(dir, "*.csv", SearchOption.AllDirectories))
            {
                var file = new FileInfo(path);
                if (latest == null || file.LastWriteTimeUtc > latest.LastWriteTimeUtc) latest = file;
            }
        }
        if (latest == null)
        {
            response.StatusCode = 404;
            Write(response, "text/plain", "Nenhum CSV encontrado. Exporte a vista Moneyball no FM27 com F8.");
            return;
        }
        response.Headers["X-Export-Id"] = Uri.EscapeDataString(latest.Name + "|" + latest.LastWriteTimeUtc.Ticks + "|" + latest.Length);
        Write(response, "text/csv", File.ReadAllText(latest.FullName));
    }

    private static void Write(HttpListenerResponse response, string type, string content)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        response.ContentType = type + "; charset=utf-8";
        response.ContentLength64 = bytes.Length;
        response.OutputStream.Write(bytes, 0, bytes.Length);
    }
}
