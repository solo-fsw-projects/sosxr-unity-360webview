using UnityEngine;


namespace SOSXR.ConfigData.Samples
{
    [CreateAssetMenu(fileName = "Demo Config Data", menuName = "SOSXR/Config Data/Demo Config Data")]
    public class DemoConfigData : BaseConfigData
    {
        public enum Ordering
        {
            InOrder,
            Random,
            Permutation,
            Counterbalanced
        }


        [SerializeField] private string m_baseURL = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeA";
        [SerializeField] [TextArea] private string m_queryStringURL = "";
        [SerializeField] private string m_taskName = "TaskToDo";
        [SerializeField] private string m_videoName = "VideoName";
        [SerializeField] private int m_ppn = -1;
        [SerializeField] private bool m_showDebug = false;
        [SerializeField] [Range(0, 30)] private int m_debugUpdateInterval = 1;
        [SerializeField] private string m_clipDirectory = "/Users/Mine/Videos";
        [SerializeField] private string[] m_extensions = {".mp4"};
        [SerializeField] private Ordering m_order = Ordering.Counterbalanced;
        [SerializeField] private bool m_showKeyboard = false;
        [SerializeField] private bool m_repeat = false;
        [SerializeField] private Vector3 m_position = new(0, 0, 0);


        public string BaseURL
        {
            get => m_baseURL;
            set
            {
                if (value == m_baseURL)
                {
                    return;
                }

                m_baseURL = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }


        public string QueryStringURL
        {
            get => m_queryStringURL;
            set
            {
                if (value == m_queryStringURL)
                {
                    return;
                }

                m_queryStringURL = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }


        public string TaskName
        {
            get => m_taskName;
            set
            {
                if (value == m_taskName)
                {
                    return;
                }

                m_taskName = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public string VideoName
        {
            get => m_videoName;
            set
            {
                if (value == m_videoName)
                {
                    return;
                }

                m_videoName = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public int PPN
        {
            get => m_ppn;
            set
            {
                if (value == m_ppn)
                {
                    return;
                }

                m_ppn = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public string PPNString
        {
            get => m_ppn.ToString();
            set
            {
                if (int.TryParse(value, out var result))
                {
                    if (result == m_ppn)
                    {
                        return;
                    }

                    m_ppn = result;
                    // HandleConfigData.AmendConfigJsonFile(this);
                }
                else
                {
                    Debug.LogError("Could not parse PPN to int.");
                }
            }
        }

        public bool ShowDebug
        {
            get => m_showDebug;
            set
            {
                if (value == m_showDebug)
                {
                    return;
                }

                m_showDebug = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public int DebugUpdateInterval
        {
            get => m_debugUpdateInterval;
            set
            {
                if (value == m_debugUpdateInterval)
                {
                    return;
                }

                m_debugUpdateInterval = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public string ClipDirectory
        {
            get => m_clipDirectory;
            set
            {
                if (value == m_clipDirectory)
                {
                    return;
                }

                m_clipDirectory = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public string[] Extensions
        {
            get => m_extensions;
            set
            {
                if (value == m_extensions)
                {
                    return;
                }

                m_extensions = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public Ordering Order
        {
            get => m_order;
            set
            {
                if (value == m_order)
                {
                    return;
                }

                m_order = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public bool ShowKeyboard
        {
            get => m_showKeyboard;
            set
            {
                if (value == m_showKeyboard)
                {
                    return;
                }

                m_showKeyboard = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public bool Repeat
        {
            get => m_repeat;
            set
            {
                if (value == m_repeat)
                {
                    return;
                }

                m_repeat = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }

        public Vector3 Position
        {
            get => m_position;
            set
            {
                if (value == m_position)
                {
                    return;
                }

                m_position = value;
                // HandleConfigData.AmendConfigJsonFile(this);
            }
        }
    }
}