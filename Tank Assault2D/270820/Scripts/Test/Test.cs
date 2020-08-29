//using UnityEngine;
//using System.Collections;


//[RequireComponent(typeof(NavMeshAgent))]                                              //проверка на наличие компонента на объекте
//public class MoveForNM : MonoBehaviour
//{
//    private GameObject _obj;                                                                        //переменная объекта можно не использовать но пока обращаюсь конкретно к объекту
//    private NavMeshAgent _agent;                                                            // собственно сам нав меш агент
//    private Vector3 _curPos;                                                                        // Текущая позиция объекта
//    private float _angleToTrg;                                                                      //Переменная угла в сторону цели
//    private float _lastAngle = 0;                                                           //переменная предыдущего угла
//    private float _speedAgent;                                                                      //сохраняю значение изначальной скорости агента и настроек

//    // Use this for initialization

//    void Start()
//    {
//        _obj = this.gameObject;                                                                //можно было обойтись без него, и обращаться к transform например
//                                                                                               //но в дальнейшем возможно скрипт может быть назначен пустышке т.е. не конкретному объекту
//        _agent = _obj.GetComponent<NavMeshAgent>();                     //специально беру компонент с определенного ранее объекта
//        _curPos = _agent.steeringTarget;                                                //при старте значение точки пути равно текущему положению объекта
//                                                                                        //_agent.steeringTarget хранит текущую точку в пути следования(не путать с концом пути)

//        _speedAgent = _agent.speed;                                                             //храню скорость
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(_curPos != _agent.steeringTarget) {                                 //ести текущая позиция отлична от следующей точки пути то...
//                                                                               //Debug.Log ("Trg= " + _curPos);
//            _agent.velocity = new Vector3(0, 0, 0);                           //останавливаю агента (установка скорости в ноль не останавливала!?)
//                                                                              //отключение updatePosition тоже не останавливает агента он просто телепортиться)
//            if(!IsRotate(_obj.transform.forward, _agent.steeringTarget)) {        //проверка на вращение агента
//                _agent.speed = _speedAgent;                                             //вернуть скорость агенту
//                _curPos = _agent.steeringTarget;                                //записать новое текущее положение
//            }
//            //Debug.Log ("Angl = " + _lastAngle);

//        }
//    }

//    bool IsRotate(Vector3 objVec, Vector3 trgPos)
//    {                         //Функция проверки вращения агента возвращает ТРУ если крутиться
//        _angleToTrg = Vector3.Angle(objVec, trgPos);                    //Определяю угол между текущим направлением и направлением на целевую точку
//        _angleToTrg = Mathf.Round(_angleToTrg * 10f);                  //для повышения точности округляю значение угла до десятых
//                                                                       //(не смог округлить через Mathf.Round(значение,знаки) нет такой перегрузки метода
//        if(_lastAngle == _angleToTrg) {                                                   //сравниваю предыдущий угол с текущим если перестал изменяться значит...
//            return false;                                                                           //вращение остановилось
//                                                                                                    //Debug.Log ("остановился");
//        } else {
//            _lastAngle = _angleToTrg;                                                       //записываю в глобальную переменную предыдущий угол (не планирую наследников класса)
//            return true;                                                                            //иначе все еще вращается
//                                                                                                    //Debug.Log ("движется");
//        }

//    }

//}
