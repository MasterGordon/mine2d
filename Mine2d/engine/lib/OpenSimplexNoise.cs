// https://gist.github.com/jstanden/1489447
namespace Mine2d.engine.lib;

public class OpenSimplexNoise
{
    private readonly int[] a = new int[3];
    private float s, u, v, w;
    private int i, j, k;
    private readonly float onethird = 0.333333333f;
    private readonly float onesixth = 0.166666667f;
    private readonly int[] t;

    public OpenSimplexNoise()
    {
        if (this.t == null)
        {
            var rand = new Random();
            this.t = new int[8];
            for (var q = 0; q < 8; q++)
            {
                this.t[q] = rand.Next();
            }
        }
    }

    public OpenSimplexNoise(string seed)
    {
        this.t = new int[8];
        var seed_parts = seed.Split(new char[] { ' ' });

        for (var q = 0; q < 8; q++)
        {
            int b;
            try
            {
                b = int.Parse(seed_parts[q]);
            }
            catch
            {
                b = 0x0;
            }
            this.t[q] = b;
        }
    }

    public OpenSimplexNoise(int[] seed)
    { // {0x16, 0x38, 0x32, 0x2c, 0x0d, 0x13, 0x07, 0x2a}
        this.t = seed;
    }

    public string GetSeed()
    {
        var seed = "";

        for (var q = 0; q < 8; q++)
        {
            seed += this.t[q].ToString();
            if (q < 7)
                seed += " ";
        }

        return seed;
    }

    public float coherentNoise(float x, float y, float z, int octaves = 1, int multiplier = 25, float amplitude = 0.5f, float lacunarity = 2, float persistence = 0.9f)
    {
        var v3 = new Vector3(x, y, z) / multiplier;
        float val = 0;
        for (var n = 0; n < octaves; n++)
        {
            val += this.noise(v3.X, v3.Y, v3.Z) * amplitude;
            v3 *= lacunarity;
            amplitude *= persistence;
        }
        return val;
    }

    private static int Lerp(int a, int b, float t)
    {
        return (int)(a + (t * (b - a)));
    }

    public int getDensity(Vector3 loc)
    {
        var val = this.coherentNoise(loc.X, loc.Y, loc.Z);
        return Lerp(0, 255, val);
    }

    // Simplex Noise Generator
    public float noise(float x, float y, float z)
    {
        this.s = (x + y + z) * this.onethird;
        this.i = fastfloor(x + this.s);
        this.j = fastfloor(y + this.s);
        this.k = fastfloor(z + this.s);

        this.s = (this.i + this.j + this.k) * this.onesixth;
        this.u = x - this.i + this.s;
        this.v = y - this.j + this.s;
        this.w = z - this.k + this.s;

        this.a[0] = 0; this.a[1] = 0; this.a[2] = 0;

        var hi = this.u >= this.w ? this.u >= this.v
            ? 0
            : 1 : this.v >= this.w ? 1 : 2;
        var lo = this.u < this.w ? this.u < this.v
            ? 0
            : 1 : this.v < this.w ? 1 : 2;

        return this.kay(hi) + this.kay(3 - hi - lo) + this.kay(lo) + this.kay(0);
    }

    private float kay(int a)
    {
        this.s = (this.a[0] + this.a[1] + this.a[2]) * this.onesixth;
        var x = this.u - this.a[0] + this.s;
        var y = this.v - this.a[1] + this.s;
        var z = this.w - this.a[2] + this.s;
        var t = 0.6f - (x * x) - (y * y) - (z * z);
        var h = this.shuffle(this.i + this.a[0], this.j + this.a[1], this.k + this.a[2]);
        this.a[a]++;
        if (t < 0)
        {
            return 0;
        }

        var b5 = h >> 5 & 1;
        var b4 = h >> 4 & 1;
        var b3 = h >> 3 & 1;
        var b2 = h >> 2 & 1;
        var b1 = h & 3;

        var p = b1 == 1 ? x : b1 == 2 ? y : z;
        var q = b1 == 1 ? y : b1 == 2 ? z : x;
        var r = b1 == 1 ? z : b1 == 2 ? x : y;

        p = b5 == b3 ? -p : p;
        q = b5 == b4 ? -q : q;
        r = b5 != (b4 ^ b3) ? -r : r;
        t *= t;
        return 8 * t * t * (p + (b1 == 0 ? q + r : b2 == 0 ? q : r));
    }

    private int shuffle(int i, int j, int k)
    {
        return this.b(i, j, k, 0) + this.b(j, k, i, 1) + this.b(k, i, j, 2) + this.b(i, j, k, 3) + this.b(j, k, i, 4) + this.b(k, i, j, 5) + this.b(i, j, k, 6) + this.b(j, k, i, 7);
    }

    private int b(int i, int j, int k, int B)
    {
        return this.t[b(i, B) << 2 | b(j, B) << 1 | b(k, B)];
    }

    private static int b(int N, int B)
    {
        return N >> B & 1;
    }

    private static int fastfloor(float n)
    {
        return n > 0 ? (int)n : (int)n - 1;
    }
}
