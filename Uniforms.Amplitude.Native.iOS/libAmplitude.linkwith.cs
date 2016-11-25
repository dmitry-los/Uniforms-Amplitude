using System;
using ObjCRuntime;

[assembly: LinkWith ("libAmplitude.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true, LinkerFlags = "-lz -lsqlite3", IsCxx = true)]
