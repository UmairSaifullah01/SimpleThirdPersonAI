using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(IKDriver))]
public class IKDriverEditor : Editor {

	float minValue = -1.0f;
	float minLimit = -3.0f;
	float maxValue = 1.0f;
	float maxLimit = 3.0f;

	float minValueSpeed = 0.001f;
	float minLimitSpeed = 0f;
	float maxValueSpeed = 5.0f;
	float maxLimitSpeed = 10.0f;

	bool showCurrentIKDriverTargets;
	bool showCurrentIKTargetObjects;
	bool showIKSteeringWheelTargets;
	bool showOtherIKTargetObjects;

	public override void OnInspectorGUI(){

		IKDriver rg_ikDriver = (IKDriver)target;
		minValue = rg_ikDriver.maxLookLeft;
		maxValue = rg_ikDriver.maxLookRight;
		minValueSpeed = rg_ikDriver.minLookSpeed;
		maxValueSpeed = rg_ikDriver.maxLookSpeed;
		EditorGUILayout.BeginVertical("Box");
		///
		SerializedProperty ikActive = serializedObject.FindProperty("ikActive");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(ikActive, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		///
		SerializedProperty mobile = serializedObject.FindProperty("mobile");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(mobile, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();



		///
		EditorGUILayout.BeginVertical("Box");
		SerializedProperty shift = serializedObject.FindProperty("shift");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(shift, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();


		EditorGUILayout.EndVertical();
		///
		EditorGUILayout.BeginVertical("Box");
		SerializedProperty steeringWheelRotation = serializedObject.FindProperty("steeringWheelRotation");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(steeringWheelRotation, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		///
		SerializedProperty steeringWheel = serializedObject.FindProperty("steeringWheel");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(steeringWheel, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		EditorGUILayout.EndVertical();

		///
		EditorGUILayout.BeginVertical("Box");
		rg_ikDriver.avatarPosition = EditorGUILayout.Vector3Field("Avatar Position", rg_ikDriver.avatarPosition);
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField ("IK Head Look Settings");

		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField ("Look Range", GUILayout.MaxWidth(Screen.width * 0.3f));

		EditorGUILayout.MinMaxSlider( ref minValue, ref maxValue, minLimit, maxLimit );
		EditorGUILayout.EndHorizontal();
		minValue = EditorGUILayout.FloatField ("Max Look Left", minValue);
		maxValue = EditorGUILayout.FloatField ("Max Look Right", maxValue);
		EditorGUI.BeginChangeCheck();
		rg_ikDriver.maxLookLeft = minValue;
		EditorGUI.BeginChangeCheck();
		rg_ikDriver.maxLookRight = maxValue;
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		///
		SerializedProperty defaultLookXPos = serializedObject.FindProperty("defaultLookXPos");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(defaultLookXPos, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField ("Look Speed", GUILayout.MaxWidth(Screen.width * 0.3f));
		EditorGUILayout.MinMaxSlider( ref minValueSpeed, ref maxValueSpeed, minLimitSpeed, maxLimitSpeed );
		EditorGUILayout.EndHorizontal();
		minValueSpeed = EditorGUILayout.FloatField ("Steer Look Speed", minValueSpeed);
		maxValueSpeed = EditorGUILayout.FloatField ("Snap Back Speed", maxValueSpeed);
		rg_ikDriver.minLookSpeed = minValueSpeed;
		rg_ikDriver.maxLookSpeed = maxValueSpeed;
		EditorGUILayout.EndVertical();
		//EditorGUILayout.LabelField ("Right: " + maxValue.ToString("F3"), GUILayout.MaxWidth(Screen.width * 0.3f));
		//EditorGUILayout.EndHorizontal();








		EditorGUILayout.EndVertical();


		EditorGUILayout.BeginVertical("Box");
		showCurrentIKDriverTargets = EditorGUILayout.Foldout (showCurrentIKDriverTargets, "Current IK Driver Targets");
		if (showCurrentIKDriverTargets) {
			EditorGUILayout.BeginVertical ("Box");
			///Steering Wheel Target W
			SerializedProperty targetRightHandIK = serializedObject.FindProperty ("targetRightHandIK");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (targetRightHandIK, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///Steering Wheel Target W
			SerializedProperty rightHandTarget = serializedObject.FindProperty ("rightHandTarget");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightHandTarget, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///Steering Wheel Target W
			SerializedProperty targetLeftHandIK = serializedObject.FindProperty ("targetLeftHandIK");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (targetLeftHandIK, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();

			///Steering Wheel Target W
			SerializedProperty targetRightFootIK = serializedObject.FindProperty ("targetRightFootIK");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (targetRightFootIK, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();

			///Steering Wheel Target W
			SerializedProperty targetLeftFootIK = serializedObject.FindProperty ("targetLeftFootIK");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (targetLeftFootIK, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			EditorGUILayout.EndVertical ();
		}

		showCurrentIKTargetObjects = EditorGUILayout.Foldout (showCurrentIKTargetObjects, "Current IK Target Objects");
		if (showCurrentIKTargetObjects) {
			EditorGUILayout.BeginVertical ("Box");
			///
			SerializedProperty lookObj = serializedObject.FindProperty ("lookObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (lookObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			///
			SerializedProperty rightHandObj = serializedObject.FindProperty ("rightHandObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightHandObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///
			SerializedProperty leftHandObj = serializedObject.FindProperty ("leftHandObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (leftHandObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			///
			SerializedProperty leftFootObj = serializedObject.FindProperty ("leftFootObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (leftFootObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			///
			SerializedProperty rightFootObj = serializedObject.FindProperty ("rightFootObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightFootObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			EditorGUILayout.EndVertical ();
		}

		showIKSteeringWheelTargets = EditorGUILayout.Foldout (showIKSteeringWheelTargets, "IK Steering Wheel Targets");
		if (showIKSteeringWheelTargets) {
			EditorGUILayout.BeginVertical ("Box");
			///Steering Wheel Target W
			SerializedProperty steeringW = serializedObject.FindProperty ("steeringW");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringW, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();

			///Steering Wheel Target NW
			SerializedProperty steeringNW = serializedObject.FindProperty ("steeringNW");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringNW, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target N
			SerializedProperty steeringN = serializedObject.FindProperty ("steeringN");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringN, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target NE
			SerializedProperty steeringNE = serializedObject.FindProperty ("steeringNE");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringNE, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target E
			SerializedProperty steeringE = serializedObject.FindProperty ("steeringE");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringE, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target SE
			SerializedProperty steeringSE = serializedObject.FindProperty ("steeringSE");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringSE, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target S
			SerializedProperty steeringS = serializedObject.FindProperty ("steeringS");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringS, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
		
			///Steering Wheel Target SW
			SerializedProperty steeringSW = serializedObject.FindProperty ("steeringSW");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (steeringSW, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			EditorGUILayout.EndVertical ();
		}


		showOtherIKTargetObjects = EditorGUILayout.Foldout (showOtherIKTargetObjects, "Other IK Target Objects");
		if (showOtherIKTargetObjects) {
			///
			EditorGUILayout.BeginVertical ("Box");
			SerializedProperty shiftObj = serializedObject.FindProperty ("shiftObj");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (shiftObj, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();	
			///
			SerializedProperty leftFootIdle = serializedObject.FindProperty ("leftFootIdle");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (leftFootIdle, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///
			SerializedProperty leftFootClutch = serializedObject.FindProperty ("leftFootClutch");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (leftFootClutch, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///
			SerializedProperty rightFootIdle = serializedObject.FindProperty ("rightFootIdle");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightFootIdle, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			///
			SerializedProperty rightFootBrake = serializedObject.FindProperty ("rightFootBrake");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightFootBrake, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();		
			///
			SerializedProperty rightFootGas = serializedObject.FindProperty ("rightFootGas");
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (rightFootGas, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			EditorGUILayout.EndVertical ();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndVertical();
	}

}