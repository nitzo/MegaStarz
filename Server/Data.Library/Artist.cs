//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MegaStar.Data.Library.Entities
{
    public partial class Artist
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            protected set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }
    
        public virtual System.DateTime InsertDate
        {
            get;
            protected set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Song> Songs
        {
            get
            {
                if (_songs == null)
                {
                    var newCollection = new FixupCollection<Song>();
                    newCollection.CollectionChanged += FixupSongs;
                    _songs = newCollection;
                }
                return _songs;
            }
            set
            {
                if (!ReferenceEquals(_songs, value))
                {
                    var previousValue = _songs as FixupCollection<Song>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSongs;
                    }
                    _songs = value;
                    var newValue = value as FixupCollection<Song>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSongs;
                    }
                }
            }
        }
        private ICollection<Song> _songs;

        #endregion
        #region Association Fixup
    
        private void FixupSongs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Song item in e.NewItems)
                {
                    item.Artist = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Song item in e.OldItems)
                {
                    if (ReferenceEquals(item.Artist, this))
                    {
                        item.Artist = null;
                    }
                }
            }
        }

        #endregion
    }
}
