﻿namespace CloudFoundry.Net.Vmc.Cli
{
    using System;
    using System.Collections.Generic;
    using CloudFoundry.Net.Types;
    using CloudFoundry.Net.Extensions;

    static partial class Program
    {
        const string systemHeader = @"============== System Services ==============";
        const string provisionedHeader = @"=========== Provisioned Services ============";

        static bool Services(IList<string> unparsed)
        {
            IVcapClient vc = new VcapClient();
            IEnumerable<SystemService> systemServices = vc.GetSystemServices();
            if (false == systemServices.IsNullOrEmpty())
            {
                Console.WriteLine(systemHeader);
                foreach (SystemService s in systemServices)
                {
                    Console.WriteLine("{0}     {1}     {2}", s.Vendor, s.Version, s.Description);
                }
            }

            IEnumerable<ProvisionedService> provisionedServices = vc.GetProvisionedServices();
            if (false == provisionedServices.IsNullOrEmpty())
            {
                Console.WriteLine();
                Console.WriteLine(provisionedHeader);
                foreach (ProvisionedService s in provisionedServices)
                {
                    Console.WriteLine("{0}     {1}", s.Name, s.Vendor);
                }
            }

            return true;
        }

        static bool BindService(IList<string> unparsed)
        {
            // TODO match ruby argument parsing
            if (unparsed.Count != 2)
            {
                Console.Error.WriteLine("Usage: vmc bind-service <servicename> <appname>"); // TODO usage statement standardization
                return false;
            }

            string svcname = unparsed[0];
            string appname = unparsed[1];

            IVcapClient vc = new VcapClient();
            VcapClientResult rslt = vc.BindService(svcname, appname);
            return rslt.Success;
        }

        static bool UnbindService(IList<string> unparsed)
        {
            // TODO match ruby argument parsing
            if (unparsed.Count != 2)
            {
                Console.Error.WriteLine("Usage: vmc unbind-service <servicename> <appname>"); // TODO usage statement standardization
                return false;
            }

            string svcname = unparsed[0];
            string appname = unparsed[1];

            IVcapClient vc = new VcapClient();
            VcapClientResult rslt = vc.UnbindService(svcname, appname);
            return rslt.Success;
        }

        static bool CreateService(IList<string> unparsed)
        {
            if (unparsed.Count != 2)
            {
                Console.Error.WriteLine("Usage: vmc create-service <service> <servicename>"); // TODO usage statement standardization
                return false;
            }

            string svc     = unparsed[0];
            string svcname = unparsed[1];

            IVcapClient vc = new VcapClient();
            VcapClientResult rslt = vc.CreateService(svc, svcname);
            return rslt.Success;
        }

        static bool DeleteService(IList<string> unparsed)
        {
            if (unparsed.Count != 1)
            {
                Console.Error.WriteLine("Usage: vmc delete-service <servicename>"); // TODO usage statement standardization
                return false;
            }

            string svcname = unparsed[0];

            IVcapClient vc = new VcapClient();
            VcapClientResult rslt = vc.DeleteService(svcname);
            return rslt.Success;
        }
    }
}