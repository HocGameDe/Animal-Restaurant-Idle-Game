using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Spine.Unity;
using System.Threading.Tasks;

public enum CharacterState
{
    WallkingIn,
    WaitingFood,
    Eating,
    WalkingOut
}

public class Character : MonoBehaviour
{
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_WALK = "Walk";

    public float speed = 2f;
    public float eatingTime = 5f;
    public CharacterState state;

    [SerializeField] private TextFlyEffect textFlyEffect;
    [SerializeField] private ThinkingBox thinkingBox;
    private SkeletonAnimation visual;
    private Table table;
    private CandyData candy;

    private void Awake()
    {
        visual = GetComponentInChildren<SkeletonAnimation>();
    }

    public void StartVisit()
    {
        StartCoroutine(IEStartVisit());
    }

    public IEnumerator IEStartVisit()
    {
        state = CharacterState.WallkingIn;
        thinkingBox.Init();
        float distance3 = Goto(CharacterSpawner.Instance.startPoint.transform.position);
        yield return new WaitForSeconds(distance3 / speed);

        visual.AnimationName = ANIM_IDLE;
        table = RestaurantManager.Instance.FindEmptyTable();

        candy = Gameplay.Instance.GetRandomCandy();

        if (table != null)
        {
            table.visitor = this;
            float distance = Goto(table.transform.position);
            yield return new WaitForSeconds(distance / speed);
            visual.AnimationName = ANIM_IDLE;
            thinkingBox.Show(candy);
            state = CharacterState.WaitingFood;
        }
    }

    private void OnMouseDown()
    {
        if (state != CharacterState.WaitingFood) return;
        StartEating();
    }

    public void StartEating()
    {
        if (state == CharacterState.Eating) return;
        state = CharacterState.Eating;
        thinkingBox.Hide();
        table.ShowFood(candy);

        Invoke(nameof(FinishEating), eatingTime * Random.Range(0.75f, 1.25f));
    }

    public void FinishEating()
    {
        StartCoroutine(IEFinishEating());
    }

    private IEnumerator IEFinishEating()
    {
        if (table != null)
        {
            table.visitor = null;
            table.HideFood();

            int reward = Random.Range(4, 10);
            var flyEffect = MyPoolManager.Instance.Get<TextFlyEffect>(textFlyEffect.gameObject);
            flyEffect.Fly("+" + reward, transform.position + new Vector3(0, 1f));
            GameplaySound.Instance.PlaySoundAddressable(GameplaySoundId.MoneySound);

            GameSystem.userdata.gold += reward;
            GameSystem.SaveUserDataToLocal();
        }

        float distance2 = Goto(CharacterSpawner.Instance.exitPoint.transform.position);
        yield return new WaitForSeconds(distance2 / speed);
        transform.gameObject.SetActive(false);
    }

    private float Goto(Vector3 destination)
    {
        float distance = Vector2.Distance(transform.position, destination);
        visual.AnimationName = ANIM_WALK;
        transform.DOMove(destination, distance / speed).SetEase(Ease.Linear);
        return distance;
    }
}