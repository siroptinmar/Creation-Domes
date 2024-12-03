/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{
	public class OscSimon : MonoBehaviour
	{
		#region Private Vars

		private OSCTransmitter _transmitter;

		private OSCReceiver _receiver;

        public Transform buttonSpawner;

		private const string _oscAddress = "/simon/colorIndex"; // 0=R ; 1=V; 2=B; 3=Y Also, you cam use mask in address: /example/*/

		#endregion

		#region Unity Methods

		protected virtual void Start()
		{
			// Creating a transmitter.
			_transmitter = gameObject.AddComponent<OSCTransmitter>();

			// Set remote host address.
			_transmitter.RemoteHost = "127.0.0.1";

			// Set remote port;
			_transmitter.RemotePort = 12000;


			// Creating a receiver.
			_receiver = gameObject.AddComponent<OSCReceiver>();

			// Set local port.
			_receiver.LocalPort = 7003;

			// Bind "MessageReceived" method to special address.
			_receiver.Bind(_oscAddress, MessageReceived);
		}

		protected virtual void Update()
		{
			if (_transmitter == null) return;

			// // Create message
			// var message = new OSCMessage(_oscAddress);
			// message.AddValue(OSCValue.String("Hello, world!"));
			// message.AddValue(OSCValue.Float(Random.Range(0f, 1f)));

			// // Send message
			// _transmitter.Send(message);
		}

		#endregion

		#region Protected Methods

		protected void MessageReceived(OSCMessage message)
		{   
            Debug.Log(message);
            int value;
			message.ToInt(out value);
            Debug.Log(value);
			Transform button = buttonSpawner.GetChild(value);
            button.GetComponent<Button>().onClick.Invoke();
            
			
            
		}
        public  void SendMessage(string messageToSend, int index)
		{
			if (_transmitter == null) return;

			// Create message
			var message = new OSCMessage(messageToSend);
			message.AddValue(OSCValue.Int(index));

			// Send message
			_transmitter.Send(message);
		}

		#endregion
	}
}