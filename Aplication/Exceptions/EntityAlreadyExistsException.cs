﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Exceptions
{
    public class EntityAlreadyExistsException:Exception
    {
        public EntityAlreadyExistsException(string entity) : base($"{entity} already exist.")
        {

        }

        public EntityAlreadyExistsException()
        {

        }

    }
}
