using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyControl : MonoBehaviour {
	//定义缓存变量，存储Transform实例
	private Transform m_transform; 
	//定义速度
	public float speed = 0.8f;
	//定义绕z轴的旋转量
	private float rotationz = 0.0f;
	//定义绕z轴旋转速度
	public float rotateSpeed_AxisZ =45f;
	//定义绕Y轴的旋转速度
	public float rotateSpeed_AxisY =20f;
	//定义触摸点坐标
	private Vector2 touchPosition;
	//定义屏幕宽度
	private float screenWeight;


	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		this.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		screenWeight = Screen.width;
	
	}
	
	// Update is called once per frame
	void Update () {
		//向前移动
		m_transform.Translate(new Vector3(0,0,speed*Time.deltaTime));
		//螺旋桨转动
		GameObject.Find("propeller").transform.Rotate(new Vector3(0,100f*Time.deltaTime,0));
		//获取旋转量绕Z轴的
		rotationz=this.transform.eulerAngles.z;
		if(Input.touchCount>0){
			for(int i=0;i<Input.touchCount;i++){
				//实例化当前的触摸点
				Touch touch=Input.touches[i];
				//手指没有移动或者滑动的情况
				if(	touch.phase==TouchPhase.Stationary||touch.phase==TouchPhase.Moved){
					//获取触摸点的位置
					touchPosition=touch.position;
					//触摸点在左侧
					if(touchPosition.x<screenWeight/2){
						if((rotationz<=45||rotationz>=315)){
							//飞机左倾斜
							m_transform.Rotate(new Vector3(0,0,(Time.deltaTime*rotateSpeed_AxisZ)),Space.Self);

						}
						//飞机左转
						m_transform.Rotate(new Vector3(0,-Time.deltaTime*30,0),Space.World);

					}else if(touchPosition.x>=screenWeight/2){
						if((rotationz<=45||rotationz>=315)){
							//飞机右倾斜
							m_transform.Rotate(new Vector3(0,0,(Time.deltaTime*-rotateSpeed_AxisZ)),Space.Self);

						}
						//飞机右转
						m_transform.Rotate(new Vector3(0,Time.deltaTime*30,0),Space.World);
					}
				}
				//手指离开屏幕
				else if(touch.phase==TouchPhase.Ended){
					//恢复平衡
					BackToBlance();
				}
			}
			
		}
		if(Input.touchCount==0){
			BackToBlance ();
		}
		if(Application.platform==RuntimePlatform.Android){
			if(Input.GetKeyDown(KeyCode.Home)){
				Application.Quit ();
		}
			if(Input.GetKeyDown(KeyCode.Escape)){
				Application.Quit ();
			}
	}
	}
		void BackToBlance(){
		//恢复平衡
		if((rotationz <= 180)){
			//右倾状态
				if (rotationz - 0 <= 2) {
			m_transform.Rotate(0, 0,Time.deltaTime*-1);
				}
			 else {
			//快速恢复平衡
				m_transform.Rotate(0,0,Time.deltaTime*-40);
			}

		}
		//恢复平衡
		if((rotationz > 180)){
			//右倾状态
			if (360-rotationz <= 2) {
				m_transform.Rotate (0, 0, Time.deltaTime * 1);

			} else {
				//快速恢复平衡
				m_transform.Rotate(0,0,Time.deltaTime*40);
			}
	}
		}
}
	