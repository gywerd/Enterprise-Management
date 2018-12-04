﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Net.Mail;

namespace EmDataAccess
{
    public class MailAttachment
    {
        #region Fields
        private MemoryStream stream;
        private string filename;
        private string mediaType;
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a mail attachment form a byte array
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(byte[] data, string filename)
        {
            this.stream = new MemoryStream(data);
            this.filename = filename;
            this.mediaType = MediaTypeNames.Application.Octet;
        }

        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(string data, string filename)
        {
            this.stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
            this.filename = filename;
            this.mediaType = MediaTypeNames.Text.Html;
        }

        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(MailAttachment attachment)
        {
            this.stream = attachment.Stream;
            this.filename = attachment.Filename;
            this.mediaType = attachment.MediaType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the data stream for this attachment
        /// </summary>
        public Stream Data
        {
            get
            {
                return stream;
            }
        }
        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        public string Filename
        {
            get
            {
                return filename;
            }
        }
        /// <summary>
        /// Gets the attachment type: Bytes or String
        /// </summary>
        public string MediaType
        {
            get
            {
                return mediaType;
            }
        }

        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        public Attachment File
        {
            get
            {
                return new Attachment(Data, Filename, MediaType);
            }
        }

        /// <summary>
        /// Gets the stream for this attachment
        /// </summary>
        public MemoryStream Stream
        {
            get
            {
                return stream;
            }
        }

        #endregion

    }
}