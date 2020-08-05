using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //text için gerekli
using UnityEngine.SceneManagement; //level geçişi için gerekli

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;  
    public float speed = 3;
    public int count; //skor scriptleri için tam sayı değeri
    public Text counText; //skoru yazdıran text
    public Text LevelText; // birden fazla level koyacaksan level yazısı
    private int level = 1; // bu birden fazla level koyacaksan
    int health = 20; //sağlık scripti için değer
    public Text HealthBar; // sağlığını bildiren text
    public Text winText; //bu görev bittiğinde yazılacak yazı

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //rigidbody ye erişmek için
        DontDestroyOnLoad(this.gameObject); //bu 2. level eklersen player yok olmasın diye. cameracontroller.cs ye de eklenecek
        LevelText.text = "level " + level.ToString(); // level yazısını yazdırmak için
        Debug.Log(health); //kalan canı konsola aktarmak için yazdım lazım değil
        HealthBar.text = "HP : " + health; // health değerini başta eklemek için

    }

    private void Update()
    {
        HealthBar.text = "HP : " + health; //health değerini sürekli olarak güncellemek için
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // 36- 41 arası kodlar hareket kodları
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pick up")) // parantez içine yazacağın tagı toplanabilir nesneye verip collider dan triggerı açıyosun
        {
            count++; // skor değerini 1 arttırır
            Debug.Log(count + " nesne toplandı"); 
            other.gameObject.SetActive(false);  // other. toplanabilir nesneyi ifade ettiği için onu yok eden kod bu
            counText.text = "Score: " + count.ToString(); //skoru her toplamada güncelleyen kod
            if (level == 2) //bunu yapmak zorunda değilsin 2. levelde 1. den az para toplanıyor diye yaptığım bi if else bu
            {
                if (count == 20)
                {
                    winText.text = "görev tamamlandı"; // bunu if'in içine görev koşulunu yazarak trigger'a ekle. wintexti boş bırak oyun sonunda ekrana yazdırıyor
                }
            }
            else if (count == 25) 
            {

                SceneManager.LoadScene(level); //burası level atlama ekranı. scene ler 0 dan başladığı için 2. leveli yüklemek için paranteze 1 yazılıyor o yüzden level yazmak yeterli
                level++; // level atladığını kaydeden script
                count = -1; // bunu 0 yapabilirsin. level atladığında skor sayacını sıfırlar
                gameObject.transform.position = new Vector3(2, 1, -23); // içerideki koordinatlar ışınlamak istediğin yerin koordinatları olacak. aynısını restartta da kullanabilirsin
                LevelText.text = "level " + level.ToString(); //leveli güncelliyor
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy")) // burayı duvarlara çarpma durumu için açtım.
        {
            health = health - 1; //canı 1 azaltır.
            Debug.Log(health);


            if (health == 0) //can bitince;
            {
                Debug.Log("GameOver"); 
                SceneManager.LoadScene(level - 1);
                count = -1; //burası normalde 0 olur. skor sıfırlandı manasında.
                health = 20; //health normal değere döndürülür
                level = 2; //buraya öldüğümüzde doğacağımız leveli yazıyosun
                gameObject.transform.position = new Vector3(2, 1, -23); // yine spawnpoint kodu.
            }

        }
    }
}
