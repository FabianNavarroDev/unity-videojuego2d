using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class GestorMonedas : MonoBehaviour
{
    public static GestorMonedas instancia;
    public TextMeshProUGUI textoMonedas;
    private int totalMonedas = 0;
    private FirebaseFirestore db;
    private string jugadorId = "jugador1";

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            db = FirebaseFirestore.DefaultInstance;
            CargarMonedas();
        });
    }

    public void AgregarMoneda()
    {
        totalMonedas++;
        ActualizarUI();
        GuardarMonedas();
    }

    void ActualizarUI()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + totalMonedas;
    }

    void GuardarMonedas()
    {
        DocumentReference docRef = db.Collection("jugadores").Document(jugadorId);
        docRef.SetAsync(new { monedas = totalMonedas });
    }

    void CargarMonedas()
    {
        DocumentReference docRef = db.Collection("jugadores").Document(jugadorId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                totalMonedas = snapshot.GetValue<int>("monedas");
                ActualizarUI();
            }
        });
    }
}