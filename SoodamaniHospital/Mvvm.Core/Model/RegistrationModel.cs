using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Mvvm.Core.Model
{
    public class RegistrationModel : NotifiableObject
    {
        #region Properties


        #region ID

        private int id;

        /// <summary>
        /// Gets or sets the ID for the Registration
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                this.Set<int>(() => ID, ref id, value);
            }
        }


        #endregion

        #region Name

        private string name;

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.Set<string>(() => Name, ref name, value);
            }
        }


        #endregion

        #region PhoneNumber

        private uint phoneNumber;

        /// <summary>
        /// Gets and sets the Phone number
        /// </summary>
        public uint PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                this.Set<uint>(() => PhoneNumber, ref phoneNumber, value);
            }
        }


        #endregion


        #region Address


        private string address;

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                this.Set<string>(() => Address, ref address, value);
            }
        }


        #endregion


        #region BloodGroup

        private string bloodGroup;

        /// <summary>
        /// Gets or sets BloodGroub
        /// </summary>
        public string BloodGroup
        {
            get
            {
                return bloodGroup;
            }
            set
            {
                bloodGroup = value;
            }
        }



        #endregion

        #region EmailAddress

        private string emailAddress;

        /// <summary>
        /// Gets or sets the EmailAddress
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                this.Set<string>(() => EmailAddress, ref emailAddress, value);
            }
        }


        #endregion

        #endregion
    }
}
