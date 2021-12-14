﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IRepositorWrapper
    {
        IEmployeesRepository Employees { get; }

        IVolunteersRepository Volunteers { get; }

        IUsersRepository Users { get; }

        IVehiclesRepository Vehicles { get; }

        IMaterialsRepository Materials { get; }

        IMedicinesRepository Medicines { get; }

        IEstatesRepository Estates { get; }
        IEmergenciesDisastersRepository EmergenciesDisasters { get; }

        ITypesEmergenciesDisastersRepository TypesEmergenciesDisasters { get; }

        IResources_RequestRepository Resources_Requests { get; }

        IChatRoomsRepository ChatRooms { get; }
        IChatRepository Chat { get; }


        // void Save();
    }
}
