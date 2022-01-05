﻿using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public bool GenerateNote(UserNote notes,long UserId);
        IEnumerable<Note> GetAllNotes();
        UserNote UpdateNotes(UserNote notes,long UserId,long  Noteid);
        bool DeleteNotes(long id);
        string PinORUnPinNote(long noteid);
        string TrashOrRestoreNote(long noteid);
        string ColorNote(long noteId, string color);
        string ArchiveORUnarchiveNote(long noteid);
    }
}
