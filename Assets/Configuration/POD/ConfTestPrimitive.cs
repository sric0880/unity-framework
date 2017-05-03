public class ConfTestPrimitive {

	[Min(19), Max(59)]
	public int mInt;
	[Min(0.9), Max(10.9)]
	public float mFloat;
	[Min(0.1), Max(0.99)]
	public double mDouble;
	public bool mBool;
	[Min(0), Max(0)]
	public byte mByte;
	[Require]
	public short mShort;
	[Require]
	public long mLong;
	[Require]
	public decimal mDec;
	public System.DateTime mDataTime;
	[Locale, Require]
	public string mStr;

}
