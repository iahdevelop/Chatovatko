﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission
{
    public enum ConnectionCommand
    {
        /// <summary>
        /// Client wants to delete contact from Trusted Contacts.
        /// No AES key will be deleted.
        /// </summary>
        UNTRUST_CONTACT = 0,
        /// <summary>
        /// The client wants to add a contact to Trusted Contacts. 
        /// If no AES key has been created already, it will be created.
        /// </summary>
        TRUST_CONTACT = 1,

        /// <summary>
        /// Client wants to push.
        /// </summary>
        PUSH = 5,
        /// <summary>
        /// Client wants to pull.
        /// </summary>
        PULL = 6,

        END_CONNECTION = 10
    }
}
