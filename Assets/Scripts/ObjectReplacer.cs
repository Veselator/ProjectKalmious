using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    // Класс, который должен менять активный объект зависимо от выбора игрока

    [SerializeField] private GameObject[] _objects;
    [SerializeField] private PlayerChoiceType _choiceType;

    private void Start()
    {
        int choice = GameSetup.Instance[_choiceType];
        Apply(choice);
    }

    private void Apply(int index)
    {
        _objects[index].SetActive(true);
    }
}