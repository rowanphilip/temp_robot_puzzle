public class BackgroundGenerator : MonoBehaviour
{
    public GameObject2D obstacle;
    public Vector2[] easy_map;
    public Vector2[] medium_map;
    public Vector2[] difficult_map;

    public Vector2 reference_corner;
    public float tile_size;

    public enum Difficulty {
        eNotSet,
        eEasy,
        eMedium,
        eHard
    }

    private Difficulty difficulty;

    public SetDifficulty(Difficulty difficulty) {
        this.difficulty = difficulty;
        ReGenerate();
    }

    void Start() {
        if (this.difficulty != eNotSet) {
            ReGenerate();
        }
    }

    private ReGenerate() {
        Vector2[] current_map;

        switch (this.difficulty) {
            case eNotSet:
                return;
            case eEasy:
                current_map = easy_map;
                break;
            case eMedium:
                current_map = medium_map;
                break;
            case eHard:
                current_map = hard_map;
                break;
        }

        foreach (Vector2 obstacle_location in current_map) {
            Vector2 translated_location = obstacle_location * tile_size + reference_corner;
            Instantiate(obstacle, translated_location, Quaternion.identity);
        }
    }
}