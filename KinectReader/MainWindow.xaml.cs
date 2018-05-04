using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;

namespace KinectReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor kinectSensor;
        public InfraredFrameReader frameReader = null;
        public FaceFrameHandler faceFrameHandler;
        public VolumeHandler volumeHandler;

        /// <summary>
        /// Reader for color frames
        /// </summary>
        private ColorFrameReader colorFrameReader = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;

        BodyFrameHandler bodyFrameHandler;

        public ConnectorHub.ConnectorHub myConectorHub;

        public bool isRecording = false;


        public MainWindow()
        {
            InitializeComponent();

            this.kinectSensor = KinectSensor.GetDefault();

            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // wire handler for frame arrival
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;

            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            // create the bitmap to display
            this.colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);


            this.kinectSensor.Open();
            this.frameReader = this.kinectSensor.InfraredFrameSource.OpenReader();

            bodyFrameHandler = new BodyFrameHandler(this.kinectSensor, this);
            faceFrameHandler = new FaceFrameHandler(this.kinectSensor);
            volumeHandler = new VolumeHandler(this.kinectSensor);


            try
            {
                myConectorHub = new ConnectorHub.ConnectorHub();

                myConectorHub.init();
                myConectorHub.sendReady();
                setValuesNames();
                myConectorHub.startRecordingEvent += MyConectorHub_startRecordingEvent;
                myConectorHub.stopRecordingEvent += MyConectorHub_stopRecordingEvent;


            }
            catch (Exception e)
            {
                int x = 1;
            }


        }

        private void MyConectorHub_stopRecordingEvent(object sender)
        {
            isRecording = false;
        }

        private void MyConectorHub_startRecordingEvent(object sender)
        {
            isRecording = true;
        }

        #region setValueNames
        private void setValuesNames()
        {
            //justforTestNames = new List<string>();
            List<string> names = new List<string>();
            string temp;

            temp = "Volume";
            names.Add(temp);

            temp = "0Engaged";
            names.Add(temp);
            temp = "0Happy";
            names.Add(temp);
            temp = "0LeftEyeClosed";
            names.Add(temp);
            temp = "0LookingAway";
            names.Add(temp);
            temp = "0MouthOpen";
            names.Add(temp);
            temp = "0MouthMoved";
            names.Add(temp);
            temp = "0RightEyeClosed";
            names.Add(temp);
            temp = "0WearingGlasses";
            names.Add(temp);
         

            temp = "0AnkleRightX";
            names.Add(temp);
            temp = "0AnkleRightY";
            names.Add(temp);
            temp = "0AnkleRightZ";
            names.Add(temp);
            temp = "0AnkleLeftX";
            names.Add(temp);
            temp = "0AnkleLeftY";
            names.Add(temp);
            temp = "0AnkleLeftZ";
            names.Add(temp);
            temp = "0ElbowRightX";
            names.Add(temp);
            temp = "0ElbowRightY";
            names.Add(temp);
            temp = "0ElbowRightZ";
            names.Add(temp);
            temp = "0ElbowLeftX";
            names.Add(temp);
            temp = "0ElbowLeftY";
            names.Add(temp);
            temp = "0ElbowLeftZ";
            names.Add(temp);
            temp = "0HandRightX";
            names.Add(temp);
            temp = "0HandRightY";
            names.Add(temp);
            temp = "0HandRightZ";
            names.Add(temp);
            temp = "0HandLeftX";
            names.Add(temp);
            temp = "0HandLeftY";
            names.Add(temp);
            temp = "0HandLeftZ";
            names.Add(temp);
            temp = "0HandRightTipX";
            names.Add(temp);
            temp = "0HandRightTipY";
            names.Add(temp);
            temp = "0HandRightTipZ";
            names.Add(temp);
            temp = "0HandLeftTipX";
            names.Add(temp);
            temp = "0HandLeftTipY";
            names.Add(temp);
            temp = "0HandLeftTipZ";
            names.Add(temp);
            temp = "0HeadX";
            names.Add(temp);
            temp = "0HeadY";
            names.Add(temp);
            temp = "0HeadZ";
            names.Add(temp);
            temp = "0HipRightX";
            names.Add(temp);
            temp = "0HipRightY";
            names.Add(temp);
            temp = "0HipRightZ";
            names.Add(temp);
            temp = "0HipLeftX";
            names.Add(temp);
            temp = "0HipLeftY";
            names.Add(temp);
            temp = "0HipLeftZ";
            names.Add(temp);
            temp = "0ShoulderRightX";
            names.Add(temp);
            temp = "0ShoulderRightY";
            names.Add(temp);
            temp = "0ShoulderRightZ";
            names.Add(temp);
            temp = "0ShoulderLeftX";
            names.Add(temp);
            temp = "0ShoulderLeftY";
            names.Add(temp);
            temp = "0ShoulderLeftZ";
            names.Add(temp);
            temp = "0SpineMidX";
            names.Add(temp);
            temp = "0SpineMidY";
            names.Add(temp);
            temp = "0SpineMidZ";
            names.Add(temp);
            temp = "0SpineShoulderX";
            names.Add(temp);
            temp = "0SpineShoulderY";
            names.Add(temp);
            temp = "0SpineShoulderZ";
            names.Add(temp);


            temp = "1Engaged";
            names.Add(temp);
            temp = "1Happy";
            names.Add(temp);
            temp = "1LeftEyeClosed";
            names.Add(temp);
            temp = "1LookingAway";
            names.Add(temp);
            temp = "1MouthOpen";
            names.Add(temp);
            temp = "1MouthMoved";
            names.Add(temp);
            temp = "1RightEyeClosed";
            names.Add(temp);
            temp = "1WearingGlasses";
            names.Add(temp);

            temp = "1AnkleRightX";
            names.Add(temp);
            temp = "1AnkleRightY";
            names.Add(temp);
            temp = "1AnkleRightZ";
            names.Add(temp);
            temp = "1AnkleLeftX";
            names.Add(temp);
            temp = "1AnkleLeftY";
            names.Add(temp);
            temp = "1AnkleLeftZ";
            names.Add(temp);
            temp = "1ElbowRightX";
            names.Add(temp);
            temp = "1ElbowRightY";
            names.Add(temp);
            temp = "1ElbowRightZ";
            names.Add(temp);
            temp = "1ElbowLeftX";
            names.Add(temp);
            temp = "1ElbowLeftY";
            names.Add(temp);
            temp = "1ElbowLeftZ";
            names.Add(temp);
            temp = "1HandRightX";
            names.Add(temp);
            temp = "1HandRightY";
            names.Add(temp);
            temp = "1HandRightZ";
            names.Add(temp);
            temp = "1HandLeftX";
            names.Add(temp);
            temp = "1HandLeftY";
            names.Add(temp);
            temp = "1HandLeftZ";
            names.Add(temp);
            temp = "1HandRightTipX";
            names.Add(temp);
            temp = "1HandRightTipY";
            names.Add(temp);
            temp = "1HandRightTipZ";
            names.Add(temp);
            temp = "1HandLeftTipX";
            names.Add(temp);
            temp = "1HandLeftTipY";
            names.Add(temp);
            temp = "1HandLeftTipZ";
            names.Add(temp);
            temp = "1HeadX";
            names.Add(temp);
            temp = "1HeadY";
            names.Add(temp);
            temp = "1HeadZ";
            names.Add(temp);
            temp = "1HipRightX";
            names.Add(temp);
            temp = "1HipRightY";
            names.Add(temp);
            temp = "1HipRightZ";
            names.Add(temp);
            temp = "1HipLeftX";
            names.Add(temp);
            temp = "1HipLeftY";
            names.Add(temp);
            temp = "1HipLeftZ";
            names.Add(temp);
            temp = "1ShoulderRightX";
            names.Add(temp);
            temp = "1ShoulderRightY";
            names.Add(temp);
            temp = "1ShoulderRightZ";
            names.Add(temp);
            temp = "1ShoulderLeftX";
            names.Add(temp);
            temp = "1ShoulderLeftY";
            names.Add(temp);
            temp = "1ShoulderLeftZ";
            names.Add(temp);
            temp = "1SpineMidX";
            names.Add(temp);
            temp = "1SpineMidY";
            names.Add(temp);
            temp = "1SpineMidZ";
            names.Add(temp);
            temp = "1SpineShoulderX";
            names.Add(temp);
            temp = "1SpineShoulderY";
            names.Add(temp);
            temp = "1SpineShoulderZ";
            names.Add(temp);

            temp = "2Engaged";
            names.Add(temp);
            temp = "2Happy";
            names.Add(temp);
            temp = "2LeftEyeClosed";
            names.Add(temp);
            temp = "2LookingAway";
            names.Add(temp);
            temp = "2MouthOpen";
            names.Add(temp);
            temp = "2MouthMoved";
            names.Add(temp);
            temp = "2RightEyeClosed";
            names.Add(temp);
            temp = "2WearingGlasses";
            names.Add(temp);

            temp = "2AnkleRightX";
            names.Add(temp);
            temp = "2AnkleRightY";
            names.Add(temp);
            temp = "2AnkleRightZ";
            names.Add(temp);
            temp = "2AnkleLeftX";
            names.Add(temp);
            temp = "2AnkleLeftY";
            names.Add(temp);
            temp = "2AnkleLeftZ";
            names.Add(temp);
            temp = "2ElbowRightX";
            names.Add(temp);
            temp = "2ElbowRightY";
            names.Add(temp);
            temp = "2ElbowRightZ";
            names.Add(temp);
            temp = "2ElbowLeftX";
            names.Add(temp);
            temp = "2ElbowLeftY";
            names.Add(temp);
            temp = "2ElbowLeftZ";
            names.Add(temp);
            temp = "2HandRightX";
            names.Add(temp);
            temp = "2HandRightY";
            names.Add(temp);
            temp = "2HandRightZ";
            names.Add(temp);
            temp = "2HandLeftX";
            names.Add(temp);
            temp = "2HandLeftY";
            names.Add(temp);
            temp = "2HandLeftZ";
            names.Add(temp);
            temp = "2HandRightTipX";
            names.Add(temp);
            temp = "2HandRightTipY";
            names.Add(temp);
            temp = "2HandRightTipZ";
            names.Add(temp);
            temp = "2HandLeftTipX";
            names.Add(temp);
            temp = "2HandLeftTipY";
            names.Add(temp);
            temp = "2HandLeftTipZ";
            names.Add(temp);
            temp = "2HeadX";
            names.Add(temp);
            temp = "2HeadY";
            names.Add(temp);
            temp = "2HeadZ";
            names.Add(temp);
            temp = "2HipRightX";
            names.Add(temp);
            temp = "2HipRightY";
            names.Add(temp);
            temp = "2HipRightZ";
            names.Add(temp);
            temp = "2HipLeftX";
            names.Add(temp);
            temp = "2HipLeftY";
            names.Add(temp);
            temp = "2HipLeftZ";
            names.Add(temp);
            temp = "2ShoulderRightX";
            names.Add(temp);
            temp = "2ShoulderRightY";
            names.Add(temp);
            temp = "2ShoulderRightZ";
            names.Add(temp);
            temp = "2ShoulderLeftX";
            names.Add(temp);
            temp = "2ShoulderLeftY";
            names.Add(temp);
            temp = "2ShoulderLeftZ";
            names.Add(temp);
            temp = "2SpineMidX";
            names.Add(temp);
            temp = "2SpineMidY";
            names.Add(temp);
            temp = "2SpineMidZ";
            names.Add(temp);
            temp = "2SpineShoulderX";
            names.Add(temp);
            temp = "2SpineShoulderY";
            names.Add(temp);
            temp = "2SpineShoulderZ";
            names.Add(temp);

            temp = "3Engaged";
            names.Add(temp);
            temp = "3Happy";
            names.Add(temp);
            temp = "3LeftEyeClosed";
            names.Add(temp);
            temp = "3LookingAway";
            names.Add(temp);
            temp = "3MouthOpen";
            names.Add(temp);
            temp = "3MouthMoved";
            names.Add(temp);
            temp = "3RightEyeClosed";
            names.Add(temp);
            temp = "3WearingGlasses";
            names.Add(temp);

            temp = "3AnkleRightX";
            names.Add(temp);
            temp = "3AnkleRightY";
            names.Add(temp);
            temp = "3AnkleRightZ";
            names.Add(temp);
            temp = "3AnkleLeftX";
            names.Add(temp);
            temp = "3AnkleLeftY";
            names.Add(temp);
            temp = "3AnkleLeftZ";
            names.Add(temp);
            temp = "3ElbowRightX";
            names.Add(temp);
            temp = "3ElbowRightY";
            names.Add(temp);
            temp = "3ElbowRightZ";
            names.Add(temp);
            temp = "3ElbowLeftX";
            names.Add(temp);
            temp = "3ElbowLeftY";
            names.Add(temp);
            temp = "3ElbowLeftZ";
            names.Add(temp);
            temp = "3HandRightX";
            names.Add(temp);
            temp = "3HandRightY";
            names.Add(temp);
            temp = "3HandRightZ";
            names.Add(temp);
            temp = "3HandLeftX";
            names.Add(temp);
            temp = "3HandLeftY";
            names.Add(temp);
            temp = "3HandLeftZ";
            names.Add(temp);
            temp = "3HandRightTipX";
            names.Add(temp);
            temp = "3HandRightTipY";
            names.Add(temp);
            temp = "3HandRightTipZ";
            names.Add(temp);
            temp = "3HandLeftTipX";
            names.Add(temp);
            temp = "3HandLeftTipY";
            names.Add(temp);
            temp = "3HandLeftTipZ";
            names.Add(temp);
            temp = "3HeadX";
            names.Add(temp);
            temp = "3HeadY";
            names.Add(temp);
            temp = "3HeadZ";
            names.Add(temp);
            temp = "3HipRightX";
            names.Add(temp);
            temp = "3HipRightY";
            names.Add(temp);
            temp = "3HipRightZ";
            names.Add(temp);
            temp = "3HipLeftX";
            names.Add(temp);
            temp = "3HipLeftY";
            names.Add(temp);
            temp = "3HipLeftZ";
            names.Add(temp);
            temp = "3ShoulderRightX";
            names.Add(temp);
            temp = "3ShoulderRightY";
            names.Add(temp);
            temp = "3ShoulderRightZ";
            names.Add(temp);
            temp = "3ShoulderLeftX";
            names.Add(temp);
            temp = "3ShoulderLeftY";
            names.Add(temp);
            temp = "3ShoulderLeftZ";
            names.Add(temp);
            temp = "3SpineMidX";
            names.Add(temp);
            temp = "3SpineMidY";
            names.Add(temp);
            temp = "3SpineMidZ";
            names.Add(temp);
            temp = "3SpineShoulderX";
            names.Add(temp);
            temp = "3SpineShoulderY";
            names.Add(temp);
            temp = "3SpineShoulderZ";
            names.Add(temp);

            temp = "4Engaged";
            names.Add(temp);
            temp = "4Happy";
            names.Add(temp);
            temp = "4LeftEyeClosed";
            names.Add(temp);
            temp = "4LookingAway";
            names.Add(temp);
            temp = "4MouthOpen";
            names.Add(temp);
            temp = "4MouthMoved";
            names.Add(temp);
            temp = "4RightEyeClosed";
            names.Add(temp);
            temp = "4WearingGlasses";
            names.Add(temp);

            temp = "4AnkleRightX";
            names.Add(temp);
            temp = "4AnkleRightY";
            names.Add(temp);
            temp = "4AnkleRightZ";
            names.Add(temp);
            temp = "4AnkleLeftX";
            names.Add(temp);
            temp = "4AnkleLeftY";
            names.Add(temp);
            temp = "4AnkleLeftZ";
            names.Add(temp);
            temp = "4ElbowRightX";
            names.Add(temp);
            temp = "4ElbowRightY";
            names.Add(temp);
            temp = "4ElbowRightZ";
            names.Add(temp);
            temp = "4ElbowLeftX";
            names.Add(temp);
            temp = "4ElbowLeftY";
            names.Add(temp);
            temp = "4ElbowLeftZ";
            names.Add(temp);
            temp = "4HandRightX";
            names.Add(temp);
            temp = "4HandRightY";
            names.Add(temp);
            temp = "4HandRightZ";
            names.Add(temp);
            temp = "4HandLeftX";
            names.Add(temp);
            temp = "4HandLeftY";
            names.Add(temp);
            temp = "4HandLeftZ";
            names.Add(temp);
            temp = "4HandRightTipX";
            names.Add(temp);
            temp = "4HandRightTipY";
            names.Add(temp);
            temp = "4HandRightTipZ";
            names.Add(temp);
            temp = "4HandLeftTipX";
            names.Add(temp);
            temp = "4HandLeftTipY";
            names.Add(temp);
            temp = "4HandLeftTipZ";
            names.Add(temp);
            temp = "4HeadX";
            names.Add(temp);
            temp = "4HeadY";
            names.Add(temp);
            temp = "4HeadZ";
            names.Add(temp);
            temp = "4HipRightX";
            names.Add(temp);
            temp = "4HipRightY";
            names.Add(temp);
            temp = "4HipRightZ";
            names.Add(temp);
            temp = "4HipLeftX";
            names.Add(temp);
            temp = "4HipLeftY";
            names.Add(temp);
            temp = "4HipLeftZ";
            names.Add(temp);
            temp = "4ShoulderRightX";
            names.Add(temp);
            temp = "4ShoulderRightY";
            names.Add(temp);
            temp = "4ShoulderRightZ";
            names.Add(temp);
            temp = "4ShoulderLeftX";
            names.Add(temp);
            temp = "4ShoulderLeftY";
            names.Add(temp);
            temp = "4ShoulderLeftZ";
            names.Add(temp);
            temp = "4SpineMidX";
            names.Add(temp);
            temp = "4SpineMidY";
            names.Add(temp);
            temp = "4SpineMidZ";
            names.Add(temp);
            temp = "4SpineShoulderX";
            names.Add(temp);
            temp = "4SpineShoulderY";
            names.Add(temp);
            temp = "4SpineShoulderZ";
            names.Add(temp);

            temp = "5Engaged";
            names.Add(temp);
            temp = "5Happy";
            names.Add(temp);
            temp = "5LeftEyeClosed";
            names.Add(temp);
            temp = "5LookingAway";
            names.Add(temp);
            temp = "5MouthOpen";
            names.Add(temp);
            temp = "5MouthMoved";
            names.Add(temp);
            temp = "5RightEyeClosed";
            names.Add(temp);
            temp = "5WearingGlasses";
            names.Add(temp);

            temp = "5AnkleRightX";
            names.Add(temp);
            temp = "5AnkleRightY";
            names.Add(temp);
            temp = "5AnkleRightZ";
            names.Add(temp);
            temp = "5AnkleLeftX";
            names.Add(temp);
            temp = "5AnkleLeftY";
            names.Add(temp);
            temp = "5AnkleLeftZ";
            names.Add(temp);
            temp = "5ElbowRightX";
            names.Add(temp);
            temp = "5ElbowRightY";
            names.Add(temp);
            temp = "5ElbowRightZ";
            names.Add(temp);
            temp = "5ElbowLeftX";
            names.Add(temp);
            temp = "5ElbowLeftY";
            names.Add(temp);
            temp = "5ElbowLeftZ";
            names.Add(temp);
            temp = "5HandRightX";
            names.Add(temp);
            temp = "5HandRightY";
            names.Add(temp);
            temp = "5HandRightZ";
            names.Add(temp);
            temp = "5HandLeftX";
            names.Add(temp);
            temp = "5HandLeftY";
            names.Add(temp);
            temp = "5HandLeftZ";
            names.Add(temp);
            temp = "5HandRightTipX";
            names.Add(temp);
            temp = "5HandRightTipY";
            names.Add(temp);
            temp = "5HandRightTipZ";
            names.Add(temp);
            temp = "5HandLeftTipX";
            names.Add(temp);
            temp = "5HandLeftTipY";
            names.Add(temp);
            temp = "5HandLeftTipZ";
            names.Add(temp);
            temp = "5HeadX";
            names.Add(temp);
            temp = "5HeadY";
            names.Add(temp);
            temp = "5HeadZ";
            names.Add(temp);
            temp = "5HipRightX";
            names.Add(temp);
            temp = "5HipRightY";
            names.Add(temp);
            temp = "5HipRightZ";
            names.Add(temp);
            temp = "5HipLeftX";
            names.Add(temp);
            temp = "5HipLeftY";
            names.Add(temp);
            temp = "5HipLeftZ";
            names.Add(temp);
            temp = "5ShoulderRightX";
            names.Add(temp);
            temp = "5ShoulderRightY";
            names.Add(temp);
            temp = "5ShoulderRightZ";
            names.Add(temp);
            temp = "5ShoulderLeftX";
            names.Add(temp);
            temp = "5ShoulderLeftY";
            names.Add(temp);
            temp = "5ShoulderLeftZ";
            names.Add(temp);
            temp = "5SpineMidX";
            names.Add(temp);
            temp = "5SpineMidY";
            names.Add(temp);
            temp = "5SpineMidZ";
            names.Add(temp);
            temp = "5SpineShoulderX";
            names.Add(temp);
            temp = "5SpineShoulderY";
            names.Add(temp);
            temp = "5SpineShoulderZ";
            names.Add(temp);


            myConectorHub.setValuesName(names);
        }
        #endregion

        public void setValues(Body[] bodies)
        {
            if(isRecording)
            {
                int counter = 0;
                List<string> values = new List<string>();
                values.Add(volumeHandler.averageVolume.ToString());

                foreach (Body body in bodies)
                {
                    try
                    {
                        values.Add(faceFrameHandler.values[counter, 0]);
                        values.Add(faceFrameHandler.values[counter, 1]);
                        values.Add(faceFrameHandler.values[counter, 2]);
                        values.Add(faceFrameHandler.values[counter, 3]);
                        values.Add(faceFrameHandler.values[counter, 4]);
                        values.Add(faceFrameHandler.values[counter, 5]);
                        values.Add(faceFrameHandler.values[counter, 6]);
                        values.Add(faceFrameHandler.values[counter, 7]);


                        values.Add(body.Joints[JointType.AnkleRight].Position.X + "");
                        values.Add(body.Joints[JointType.AnkleRight].Position.Y + "");
                        values.Add(body.Joints[JointType.AnkleRight].Position.Z + "");

                        values.Add(body.Joints[JointType.AnkleLeft].Position.X + "");
                        values.Add(body.Joints[JointType.AnkleLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.AnkleLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.ElbowRight].Position.X + "");
                        values.Add(body.Joints[JointType.ElbowRight].Position.Y + "");
                        values.Add(body.Joints[JointType.ElbowRight].Position.Z + "");

                        values.Add(body.Joints[JointType.ElbowLeft].Position.X + "");
                        values.Add(body.Joints[JointType.ElbowLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.ElbowLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.HandRight].Position.X + "");
                        values.Add(body.Joints[JointType.HandRight].Position.Y + "");
                        values.Add(body.Joints[JointType.HandRight].Position.Z + "");

                        values.Add(body.Joints[JointType.HandLeft].Position.X + "");
                        values.Add(body.Joints[JointType.HandLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.HandLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.HandTipRight].Position.X + "");
                        values.Add(body.Joints[JointType.HandTipRight].Position.Y + "");
                        values.Add(body.Joints[JointType.HandTipRight].Position.Z + "");

                        values.Add(body.Joints[JointType.HandTipLeft].Position.X + "");
                        values.Add(body.Joints[JointType.HandTipLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.HandTipLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.Head].Position.X + "");
                        values.Add(body.Joints[JointType.Head].Position.Y + "");
                        values.Add(body.Joints[JointType.Head].Position.Z + "");

                        values.Add(body.Joints[JointType.HipRight].Position.X + "");
                        values.Add(body.Joints[JointType.HipRight].Position.Y + "");
                        values.Add(body.Joints[JointType.HipRight].Position.Z + "");

                        values.Add(body.Joints[JointType.HipLeft].Position.X + "");
                        values.Add(body.Joints[JointType.HipLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.HipLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.ShoulderRight].Position.X + "");
                        values.Add(body.Joints[JointType.ShoulderRight].Position.Y + "");
                        values.Add(body.Joints[JointType.ShoulderRight].Position.Z + "");

                        values.Add(body.Joints[JointType.ShoulderLeft].Position.X + "");
                        values.Add(body.Joints[JointType.ShoulderLeft].Position.Y + "");
                        values.Add(body.Joints[JointType.ShoulderLeft].Position.Z + "");

                        values.Add(body.Joints[JointType.SpineMid].Position.X + "");
                        values.Add(body.Joints[JointType.SpineMid].Position.Y + "");
                        values.Add(body.Joints[JointType.SpineMid].Position.Z + "");

                        values.Add(body.Joints[JointType.SpineShoulder].Position.X + "");
                        values.Add(body.Joints[JointType.SpineShoulder].Position.Y + "");
                        values.Add(body.Joints[JointType.SpineShoulder].Position.Z + "");


                        if (body.Joints[JointType.ShoulderRight].Position.X != 0)
                        {
                            int xxx = counter;
                        }

                    }
                    catch
                    {

                    }
                    counter++;
                }
                  myConectorHub.storeFrame(values);
            }


            
          
        }


        public ImageSource ImageSource
        {
            get
            {
                return this.colorBitmap;
            }
        }


        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                        }

                        this.colorBitmap.Unlock();

                    }
                }

                myImage.Source = ImageSource;
            }
        }







        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.colorFrameReader != null)
            {
                // ColorFrameReder is IDisposable
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }

            if (volumeHandler != null)
            {
                volumeHandler.close();
            }
            if (faceFrameHandler != null)
            {
                faceFrameHandler.close();
            }
            if (bodyFrameHandler != null)
            {
                bodyFrameHandler.close();
            }
            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }

            
        }

    }
}
