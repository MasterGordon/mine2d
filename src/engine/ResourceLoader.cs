using System.Runtime.InteropServices;

class ResourceLoader
{
    private string assemblyName;

    public ResourceLoader()
    {
        this.assemblyName = this.GetType().Assembly.GetName().Name!;
    }

    public string LoadString(string resourceName)
    {
#if (DEBUG)
        Console.WriteLine("Loading resource: " + resourceName);
        return File.ReadAllText(ToPath(resourceName));
#endif
        using var stream = this.GetType()
            .Assembly.GetManifestResourceStream($"{this.assemblyName}.{resourceName}");
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }

    public byte[] LoadBytes(string resourceName)
    {
        using var stream = this.GetType()
            .Assembly.GetManifestResourceStream($"{this.assemblyName}.{resourceName}");
        using var memoryStream = new MemoryStream();
        stream!.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public (IntPtr ptr, int size) LoadToIntPtr(string resourceName)
    {
        var bytes = this.LoadBytes(resourceName);
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        var ptr = handle.AddrOfPinnedObject();
        return (ptr, bytes.Length);
    }

    public void SaveString(string resourceName, string content)
    {
        using var stream = new StreamWriter(ToPath(resourceName));
        stream.Write(content);
    }

    private static string ToPath(string resourceName)
    {
        var s = resourceName.Split('.');
        return String.Join('/', s[..^1]) + "." + s[^1];
    }
}
