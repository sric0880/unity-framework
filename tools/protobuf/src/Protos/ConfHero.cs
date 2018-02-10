// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: client/conf_hero.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace UF.Config {

  /// <summary>Holder for reflection information generated from client/conf_hero.proto</summary>
  public static partial class ConfHeroReflection {

    #region Descriptor
    /// <summary>File descriptor for client/conf_hero.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConfHeroReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZjbGllbnQvY29uZl9oZXJvLnByb3RvEgZjbGllbnQiuAEKCENvbmZIZXJv",
            "EhcKD2F0dGFja19zZWN0aW9ucxgBIAMoBRIcChRhdHRhY2tfd2FpdF90aW1l",
            "X21pbhgCIAMoAhIcChRhdHRhY2tfd2FpdF90aW1lX21heBgDIAMoAhIYChBh",
            "dHRhY2tfd2FpdF90aW1lGAQgAygCEhUKDWF0dGFja193ZWlnaHQYBSABKAIS",
            "FAoMc2tpbGxfd2VpZ2h0GAYgAygCEhAKCHRlc3RfYWRkGAcgASgFQgyqAglV",
            "Ri5Db25maWdiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::UF.Config.ConfHero), global::UF.Config.ConfHero.Parser, new[]{ "AttackSections", "AttackWaitTimeMin", "AttackWaitTimeMax", "AttackWaitTime", "AttackWeight", "SkillWeight", "TestAdd" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ConfHero : pb::IMessage<ConfHero> {
    private static readonly pb::MessageParser<ConfHero> _parser = new pb::MessageParser<ConfHero>(() => new ConfHero());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ConfHero> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::UF.Config.ConfHeroReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfHero() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfHero(ConfHero other) : this() {
      attackSections_ = other.attackSections_.Clone();
      attackWaitTimeMin_ = other.attackWaitTimeMin_.Clone();
      attackWaitTimeMax_ = other.attackWaitTimeMax_.Clone();
      attackWaitTime_ = other.attackWaitTime_.Clone();
      attackWeight_ = other.attackWeight_;
      skillWeight_ = other.skillWeight_.Clone();
      testAdd_ = other.testAdd_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ConfHero Clone() {
      return new ConfHero(this);
    }

    /// <summary>Field number for the "attack_sections" field.</summary>
    public const int AttackSectionsFieldNumber = 1;
    private static readonly pb::FieldCodec<int> _repeated_attackSections_codec
        = pb::FieldCodec.ForInt32(10);
    private readonly pbc::RepeatedField<int> attackSections_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> AttackSections {
      get { return attackSections_; }
    }

    /// <summary>Field number for the "attack_wait_time_min" field.</summary>
    public const int AttackWaitTimeMinFieldNumber = 2;
    private static readonly pb::FieldCodec<float> _repeated_attackWaitTimeMin_codec
        = pb::FieldCodec.ForFloat(18);
    private readonly pbc::RepeatedField<float> attackWaitTimeMin_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<float> AttackWaitTimeMin {
      get { return attackWaitTimeMin_; }
    }

    /// <summary>Field number for the "attack_wait_time_max" field.</summary>
    public const int AttackWaitTimeMaxFieldNumber = 3;
    private static readonly pb::FieldCodec<float> _repeated_attackWaitTimeMax_codec
        = pb::FieldCodec.ForFloat(26);
    private readonly pbc::RepeatedField<float> attackWaitTimeMax_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<float> AttackWaitTimeMax {
      get { return attackWaitTimeMax_; }
    }

    /// <summary>Field number for the "attack_wait_time" field.</summary>
    public const int AttackWaitTimeFieldNumber = 4;
    private static readonly pb::FieldCodec<float> _repeated_attackWaitTime_codec
        = pb::FieldCodec.ForFloat(34);
    private readonly pbc::RepeatedField<float> attackWaitTime_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<float> AttackWaitTime {
      get { return attackWaitTime_; }
    }

    /// <summary>Field number for the "attack_weight" field.</summary>
    public const int AttackWeightFieldNumber = 5;
    private float attackWeight_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float AttackWeight {
      get { return attackWeight_; }
      set {
        attackWeight_ = value;
      }
    }

    /// <summary>Field number for the "skill_weight" field.</summary>
    public const int SkillWeightFieldNumber = 6;
    private static readonly pb::FieldCodec<float> _repeated_skillWeight_codec
        = pb::FieldCodec.ForFloat(50);
    private readonly pbc::RepeatedField<float> skillWeight_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<float> SkillWeight {
      get { return skillWeight_; }
    }

    /// <summary>Field number for the "test_add" field.</summary>
    public const int TestAddFieldNumber = 7;
    private int testAdd_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int TestAdd {
      get { return testAdd_; }
      set {
        testAdd_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ConfHero);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ConfHero other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!attackSections_.Equals(other.attackSections_)) return false;
      if(!attackWaitTimeMin_.Equals(other.attackWaitTimeMin_)) return false;
      if(!attackWaitTimeMax_.Equals(other.attackWaitTimeMax_)) return false;
      if(!attackWaitTime_.Equals(other.attackWaitTime_)) return false;
      if (AttackWeight != other.AttackWeight) return false;
      if(!skillWeight_.Equals(other.skillWeight_)) return false;
      if (TestAdd != other.TestAdd) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= attackSections_.GetHashCode();
      hash ^= attackWaitTimeMin_.GetHashCode();
      hash ^= attackWaitTimeMax_.GetHashCode();
      hash ^= attackWaitTime_.GetHashCode();
      if (AttackWeight != 0F) hash ^= AttackWeight.GetHashCode();
      hash ^= skillWeight_.GetHashCode();
      if (TestAdd != 0) hash ^= TestAdd.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      attackSections_.WriteTo(output, _repeated_attackSections_codec);
      attackWaitTimeMin_.WriteTo(output, _repeated_attackWaitTimeMin_codec);
      attackWaitTimeMax_.WriteTo(output, _repeated_attackWaitTimeMax_codec);
      attackWaitTime_.WriteTo(output, _repeated_attackWaitTime_codec);
      if (AttackWeight != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(AttackWeight);
      }
      skillWeight_.WriteTo(output, _repeated_skillWeight_codec);
      if (TestAdd != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(TestAdd);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += attackSections_.CalculateSize(_repeated_attackSections_codec);
      size += attackWaitTimeMin_.CalculateSize(_repeated_attackWaitTimeMin_codec);
      size += attackWaitTimeMax_.CalculateSize(_repeated_attackWaitTimeMax_codec);
      size += attackWaitTime_.CalculateSize(_repeated_attackWaitTime_codec);
      if (AttackWeight != 0F) {
        size += 1 + 4;
      }
      size += skillWeight_.CalculateSize(_repeated_skillWeight_codec);
      if (TestAdd != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(TestAdd);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ConfHero other) {
      if (other == null) {
        return;
      }
      attackSections_.Add(other.attackSections_);
      attackWaitTimeMin_.Add(other.attackWaitTimeMin_);
      attackWaitTimeMax_.Add(other.attackWaitTimeMax_);
      attackWaitTime_.Add(other.attackWaitTime_);
      if (other.AttackWeight != 0F) {
        AttackWeight = other.AttackWeight;
      }
      skillWeight_.Add(other.skillWeight_);
      if (other.TestAdd != 0) {
        TestAdd = other.TestAdd;
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
          case 10:
          case 8: {
            attackSections_.AddEntriesFrom(input, _repeated_attackSections_codec);
            break;
          }
          case 18:
          case 21: {
            attackWaitTimeMin_.AddEntriesFrom(input, _repeated_attackWaitTimeMin_codec);
            break;
          }
          case 26:
          case 29: {
            attackWaitTimeMax_.AddEntriesFrom(input, _repeated_attackWaitTimeMax_codec);
            break;
          }
          case 34:
          case 37: {
            attackWaitTime_.AddEntriesFrom(input, _repeated_attackWaitTime_codec);
            break;
          }
          case 45: {
            AttackWeight = input.ReadFloat();
            break;
          }
          case 50:
          case 53: {
            skillWeight_.AddEntriesFrom(input, _repeated_skillWeight_codec);
            break;
          }
          case 56: {
            TestAdd = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code