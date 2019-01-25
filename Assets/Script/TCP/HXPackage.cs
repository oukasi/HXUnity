using System.Collections;
using System.Collections.Generic;
using HiSocket;
using System;

public class HXPackage : IPackage
{
    public void Unpack(IByteArray _source,Action<byte[]>_unpackHandler)
    {
        _unpackHandler(_source.Read(_source.Length));
    }
    public void Pack(IByteArray _source,Action<byte[]>_packHandler)
    {
        _packHandler(_source.Read(_source.Length));
    }
}
