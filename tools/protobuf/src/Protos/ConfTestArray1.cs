// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: client/conf_test_array1.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace UF.Config {

  /// <summary>Holder for reflection information generated from client/conf_test_array1.proto</summary>
  public static partial class ConfTestArray1Reflection {

    #region Descriptor
    /// <summary>File descriptor for client/conf_test_array1.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConfTestArray1Reflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch1jbGllbnQvY29uZl90ZXN0X2FycmF5MS5wcm90bxIGY2xpZW50IjYKDkNv",
            "bmZUZXN0QXJyYXkxEg8KB2FkZHJlc3MYASABKAkSEwoLcmFuZG9tX3BvcnQY",
            "AiADKA1CDKoCCVVGLkNvbmZpZ2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::UF.Config.ConfTestArray1), global::UF.Config.ConfTestArray1.Parser, new[]{ "Address", "RandomPort" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ConfTestArray1 : pb::IMessage<ConfTestArray1> {
    private static readonly pb::MessageParser<ConfTestArray1> _parser = new pb::MessageParser<ConfTestArray1>(() => new ConfTestArray1());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConfTestArray1> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::UF.Config.ConfTestArray1Reflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestArray1() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestArray1(ConfTestArray1 other) : this() {
      address_ = other.address_;
      randomPort_ = other.randomPort_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestArray1 Clone() {
      return new ConfTestArray1(this);
    }

    /// <summary>Field number for the "address" field.</summary>
    public const int AddressFieldNumber = 1;
    private string address_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Address {
      get { return address_; }
      set {
        address_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "random_port" field.</summary>
    public const int RandomPortFieldNumber = 2;
    private static readonly pb::FieldCodec<uint> _repeated_randomPort_codec
        = pb::FieldCodec.ForUInt32(18);
    private readonly pbc::RepeatedField<uint> randomPort_ = new pbc::RepeatedField<uint>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<uint> RandomPort {
      get { return randomPort_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConfTestArray1);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConfTestArray1 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Address != other.Address) return false;
      if(!randomPort_.Equals(other.randomPort_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Address.Length != 0) hash ^= Address.GetHashCode();
      hash ^= randomPort_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Address.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Address);
      }
      randomPort_.WriteTo(output, _repeated_randomPort_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Address.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Address);
      }
      size += randomPort_.CalculateSize(_repeated_randomPort_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConfTestArray1 other) {
      if (other == null) {
        return;
      }
      if (other.Address.Length != 0) {
        Address = other.Address;
      }
      randomPort_.Add(other.randomPort_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Address = input.ReadString();
            break;
          }
          case 18:
          case 16: {
            randomPort_.AddEntriesFrom(input, _repeated_randomPort_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
