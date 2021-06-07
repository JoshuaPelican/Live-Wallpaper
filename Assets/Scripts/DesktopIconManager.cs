using System.IO;
using UnityEngine;

public class DesktopIconManager : MonoBehaviour
{
    #region Singleton
    public static DesktopIconManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject fileIcon;
    public float UpdateIconsInterval = 300f;
    private float updateTime = 300;
    private FileInfo[] desktopFiles;

    private void Start()
    {
        updateTime += Time.deltaTime;

        if(updateTime >= UpdateIconsInterval)
        {
            updateTime = 0;
            UpdateAllDesktopFiles();
        }
    }

    private void UpdateAllDesktopFiles()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

        DirectoryInfo dir = new DirectoryInfo(desktopPath);

        if (desktopFiles != dir.GetFiles())
        {
            desktopFiles = dir.GetFiles();

            foreach (FileInfo file in desktopFiles)
            {
                string filePath = file.FullName;
                filePath = filePath.Replace("\"", "");

                if (File.Exists(filePath))
                {
                    GameObject newFileIcon = Instantiate(fileIcon, Vector3.zero, Quaternion.identity, transform);
                    newFileIcon.GetComponent<FileIcon>().filePath = filePath;
                }
            }
        }
    }
}
