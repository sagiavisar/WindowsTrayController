using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using Microsoft.VisualBasic;

namespace WindowsTrayMaster
{
    public class MyCustomApplicationContext : ApplicationContext
    {

        private NotifyIcon trayIcon;
        void RunCommand(string command)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C " + command;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
        }

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = WindowsTrayMaster.Properties.Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Useful Shortcuts", new MenuItem[] {
                    new MenuItem("Edit Environment Variables", (object sender, EventArgs e) => this.RunCommand("rundll32.exe sysdm.cpl,EditEnvironmentVariables")),
                    new MenuItem("Folders Properties", (object sender, EventArgs e) => this.RunCommand("control folders")),
                    new MenuItem("Printers and Faxes", (object sender, EventArgs e) => this.RunCommand("control printers")),
                    new MenuItem("Remote Desktop", (object sender, EventArgs e) => this.RunCommand("mstsc")),
                    new MenuItem("Scheduled Tasks", (object sender, EventArgs e) => this.RunCommand("control schedtasks")),
                    new MenuItem("Snipping Tool", (object sender, EventArgs e) => this.RunCommand("snippingtool")),
                }),
                new MenuItem("Shortcuts", new MenuItem[] {
                    new MenuItem("Add New Programs", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,1")),
                    new MenuItem("Add Remove Windows Components", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,2")),
                    new MenuItem("Set Program Access & Defaults", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,3")),
                }),
                new MenuItem("Computer Management", new MenuItem[] {
                    new MenuItem("Computer Management", (object sender, EventArgs e) => this.RunCommand("compmgmt.msc")),
                    new MenuItem("Device Manager", (object sender, EventArgs e) => this.RunCommand("devmgmt.msc")),
                    new MenuItem("Disk Management", (object sender, EventArgs e) => this.RunCommand("diskmgmt.msc")),
                    new MenuItem("Network Connections", (object sender, EventArgs e) => this.RunCommand("ncpa.cpl")),
                }),
                new MenuItem("Services", (object sender, EventArgs e) => this.RunCommand("services.msc")),
                new MenuItem("Programs and Features", (object sender, EventArgs e) => this.RunCommand("appwiz.cpl")),
                new MenuItem("Task Manager", (object sender, EventArgs e) => this.RunCommand("taskmgr")),
                new MenuItem("Control Panel", (object sender, EventArgs e) => this.RunCommand("control")),
                new MenuItem("Volume Mixer", (object sender, EventArgs e) => this.RunCommand("sndvol")),
                new MenuItem("Exit", Exit)
            }),
                Visible = true
            };
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
