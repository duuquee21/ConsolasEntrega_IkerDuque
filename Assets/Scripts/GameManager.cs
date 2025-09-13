using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- INICIO DE LA LÓGICA SINGLETON ---
    public static GameManager instance;
    public UIManager uiManager;
    private void Awake()
    {
        // Si no hay ninguna instancia asignada, esta se convierte en la instancia.
        if (instance == null)
        {
            instance = this;
        }
        // Si ya existe una instancia y no es esta, se destruye para asegurar que solo haya una.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // --- FIN DE LA LÓGICA SINGLETON ---

    private const string WAVES_URL = "https://kev-games-development.net/Services/WavesTest.json";
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int wavesToComplete = 5;

    private WavesData wavesData;
    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        // La lógica de Start() se queda igual
        StartCoroutine(GetWavesData());
    }

    // El resto de tu código (GetWavesData, SpawnWaveByTime, EnemyDefeated, etc.)
    // se queda exactamente igual. No necesitas cambiar nada más en este script.

    // ... (resto de tus funciones sin cambios)
    
    public void EnemyDefeated()
    {
        enemiesAlive--;
    }

    IEnumerator GetWavesData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(WAVES_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonText = webRequest.downloadHandler.text;
                wavesData = JsonUtility.FromJson<WavesData>(jsonText);
                
                if (wavesData == null || wavesData.Waves == null || wavesData.Waves.Count == 0)
                {
                    Debug.LogError("¡ERROR AL PARSEAR EL JSON! Revisa que WaveData.cs sea correcto.");
                    yield break;
                }
                
                StartCoroutine(RunGameLoop());
            }
            else
            {
                Debug.LogError("Error al obtener los datos de las oleadas: " + webRequest.error);
            }
        }
    }

    IEnumerator RunGameLoop()
    {
        while (currentWaveIndex < wavesToComplete && currentWaveIndex < wavesData.Waves.Count)
        {
            yield return StartCoroutine(SpawnWaveByTime(wavesData.Waves[currentWaveIndex]));
            while (enemiesAlive > 0)
            {
                yield return null;
            }
            Debug.Log($"<color=green>Oleada {currentWaveIndex + 1} completada!</color>");
            currentWaveIndex++;
        }
        Debug.Log("<color=cyan>¡Todas las oleadas completadas! VICTORIA.</color>");
        SceneManager.LoadScene("VictoryScene");
    }

    IEnumerator SpawnWaveByTime(Wave1 waveData)
    {
        Debug.Log($"<color=yellow>--- Iniciando Oleada {waveData.Wave} ---</color>");
        
        uiManager.UpdateWaveText(waveData.Wave);

        float waveTimer = 0f;
        int spawnIndex = 0;
     ;
        while (spawnIndex < waveData.Enemies.Count)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= waveData.Enemies[spawnIndex].Time)
            {
                EnemySpawnEvent currentEnemyEvent = waveData.Enemies[spawnIndex];
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
                enemiesAlive++;
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.SetEnemyType("Type" + currentEnemyEvent.Enemy);
                }
                Debug.Log($"Enemigo tipo {currentEnemyEvent.Enemy} creado a los {waveTimer:F2} segundos. Enemigos vivos: {enemiesAlive}");
                spawnIndex++;
            }
            yield return null;
        }
    }
}