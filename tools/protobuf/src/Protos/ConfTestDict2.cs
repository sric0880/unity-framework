// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: client/conf_test_dict2.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace UF.Config {

  /// <summary>Holder for reflection information generated from client/conf_test_dict2.proto</summary>
  public static partial class ConfTestDict2Reflection {

    #region Descriptor
    /// <summary>File descriptor for client/conf_test_dict2.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConfTestDict2Reflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxjbGllbnQvY29uZl90ZXN0X2RpY3QyLnByb3RvEgZjbGllbnQiUAoNQ29u",
            "ZlRlc3REaWN0MhIQCghzdHJpbmdJZBgBIAEoCRISCgpyb2xlX2xldmVsGAIg",
            "ASgFEhkKEWZpbmlzaF9kdW5nZW9uX2lkGAMgASgFQgyqAglVRi5Db25maWdi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::UF.Config.ConfTestDict2), global::UF.Config.ConfTestDict2.Parser, new[]{ "StringId", "RoleLevel", "FinishDungeonId" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ConfTestDict2 : pb::IMessage<ConfTestDict2> {
    private static readonly pb::MessageParser<ConfTestDict2> _parser = new pb::MessageParser<ConfTestDict2>(() => new ConfTestDict2());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConfTestDict2> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::UF.Config.ConfTestDict2Reflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestDict2() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestDict2(ConfTestDict2 other) : this() {
      stringId_ = other.stringId_;
      roleLevel_ = other.roleLevel_;
      finishDungeonId_ = other.finishDungeonId_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfTestDict2 Clone() {
      return new ConfTestDict2(this);
    }

    /// <summary>Field number for the "stringId" field.</summary>
    public const int StringIdFieldNumber = 1;
    private string stringId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string StringId {
      get { return stringId_; }
      set {
        stringId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "role_level" field.</summary>
    public const int RoleLevelFieldNumber = 2;
    private int roleLevel_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RoleLevel {
      get { return roleLevel_; }
      set {
        roleLevel_ = value;
      }
    }

    /// <summary>Field number for the "finish_dungeon_id" field.</summary>
    public const int FinishDungeonIdFieldNumber = 3;
    private int finishDungeonId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FinishDungeonId {
      get { return finishDungeonId_; }
      set {
        finishDungeonId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConfTestDict2);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConfTestDict2 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (StringId != other.StringId) return false;
      if (RoleLevel != other.RoleLevel) return false;
      if (FinishDungeonId != other.FinishDungeonId) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (StringId.Length != 0) hash ^= StringId.GetHashCode();
      if (RoleLevel != 0) hash ^= RoleLevel.GetHashCode();
      if (FinishDungeonId != 0) hash ^= FinishDungeonId.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (StringId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(StringId);
      }
      if (RoleLevel != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(RoleLevel);
      }
      if (FinishDungeonId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(FinishDungeonId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (StringId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StringId);
      }
      if (RoleLevel != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RoleLevel);
      }
      if (FinishDungeonId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FinishDungeonId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConfTestDict2 other) {
      if (other == null) {
        return;
      }
      if (other.StringId.Length != 0) {
        StringId = other.StringId;
      }
      if (other.RoleLevel != 0) {
        RoleLevel = other.RoleLevel;
      }
      if (other.FinishDungeonId != 0) {
        FinishDungeonId = other.FinishDungeonId;
      }
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
            StringId = input.ReadString();
            break;
          }
          case 16: {
            RoleLevel = input.ReadInt32();
            break;
          }
          case 24: {
            FinishDungeonId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
