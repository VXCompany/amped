using System;
using Amped.Core;

namespace Amped.Bookmarks.Core.Tests.UnitTests;

public class BookmarkBuilder
{
    private Uri? _uri = new("http://someuri.com");
    private bool _read;
    private string _owner = "parker";

    public BookmarkBuilder WithUri(Uri? uri)
    {
        _uri = uri;
        return this;
    }

    public BookmarkBuilder WithRead(bool read)
    {
        _read = read;
        return this;
    }

    public BookmarkBuilder WithOwner(string owner)
    {
        _owner = owner;
        return this;
    }

    public Bookmark Build() => new(_uri, _owner, _read);
}