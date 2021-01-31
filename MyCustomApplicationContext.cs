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
                new MenuItem("Power Options", new MenuItem[] {
                    new MenuItem("Lock PC", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe user32.dll,LockWorkStation")),
                    new MenuItem("Hibernate or Sleep", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe powrprof.dll,SetSuspendState")),
                    new MenuItem("Restart", (object sender, EventArgs e) => this.RunCommand("shutdown /r /f /t 0")),
                    new MenuItem("Shut Down", (object sender, EventArgs e) => this.RunCommand("shutdown /f /t 0")),
                }),
                new MenuItem("Useful Shortcuts", new MenuItem[] {
                    new MenuItem("Edit Environment Variables", (object sender, EventArgs e) => this.RunCommand("rundll32.exe sysdm.cpl,EditEnvironmentVariables")),
                    new MenuItem("Map Network Drive wizard", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe shell32.dll,SHHelpShortcuts_RunDLL Connect")),
                    
                }),
                new MenuItem("Desktop", new MenuItem[] {
                    new MenuItem("Desktop Icons", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,0")),
                    new MenuItem("Display Settings", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe shell32.dll,Control_RunDLL desk.cpl")),
                }),
                new MenuItem("Sound", new MenuItem[] {
                    new MenuItem("Recording Devices", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe shell32.dll,Control_RunDLL Mmsys.cpl,,1")),
                    new MenuItem("Playback Devices", (object sender, EventArgs e) => this.RunCommand("Rundll32.exe shell32.dll,Control_RunDLL Mmsys.cpl,,0")),
                }),
                new MenuItem("Utils", new MenuItem[] {
                    new MenuItem("Task Scheduler", (object sender, EventArgs e) => this.RunCommand("control schedtasks")),
                    new MenuItem("Remote Desktop", (object sender, EventArgs e) => this.RunCommand("mstsc")),
                    new MenuItem("Snipping Tool", (object sender, EventArgs e) => this.RunCommand("snippingtool")),
                    new MenuItem("Calculator", (object sender, EventArgs e) => this.RunCommand("calc")),
                }),

                new MenuItem("Control Panel Shortcuts", new MenuItem[] {
                    new MenuItem("Add New Programs", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,1")),
                    new MenuItem("Add Remove Windows Components", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,2")),
                    new MenuItem("Set Program Access & Defaults", (object sender, EventArgs e) => this.RunCommand("control appwiz.cpl,,3")),
                    new MenuItem("Folders Properties", (object sender, EventArgs e) => this.RunCommand("control folders")),
                    new MenuItem("Printers and Faxes", (object sender, EventArgs e) => this.RunCommand("control printers")),
                }),
                new MenuItem("Management", new MenuItem[] {
                    new MenuItem("Computer Management", (object sender, EventArgs e) => this.RunCommand("compmgmt.msc")),
                    new MenuItem("Device Management", (object sender, EventArgs e) => this.RunCommand("devmgmt.msc")),
                    new MenuItem("Disk Management", (object sender, EventArgs e) => this.RunCommand("diskmgmt.msc")),
                }),
                new MenuItem("Network Connections", (object sender, EventArgs e) => this.RunCommand("ncpa.cpl")),
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
