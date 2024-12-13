using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace LearnProject
{
    public class LessonsTileManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _tilesLayout;
        [SerializeField] private LessonTile _lessonTilePrefab;
        [SerializeField] private DataBundleForLessonTileSO[] _lessonsBundleData;

        private AsyncOperationHandle<SceneInstance> _loadHangle;

        private void Awake()
        {
            for (int i = 0; i < _lessonsBundleData.Length; i++)
            {
                SetLessonTile(_lessonsBundleData[i]);
            }
        }


        private void Start()
        {
            SceneTransitionAnimator.PlayTransitionAsync(1, 0);
        }


        private void SetLessonTile(DataBundleForLessonTileSO lessonTileData)
        {
            var tile = Instantiate(_lessonTilePrefab, _tilesLayout);
            tile.TileImage.sprite = lessonTileData.Sprite;
            tile.TileText.text = lessonTileData.LessonName;
            var isDone = Convert.ToBoolean(PlayerPrefs.GetInt(lessonTileData.LessonName));
            tile.CheckMark.enabled = isDone;
            tile.TileButton.onClick.AddListener(async () =>
            {

                await SceneTransitionAnimator.PlayTransitionAsync(0, 1);


                _loadHangle = Addressables.LoadSceneAsync(lessonTileData.Scene, LoadSceneMode.Single, false);
                _loadHangle.Completed += ((AsyncOperationHandle<SceneInstance> asyncScene) =>
                {
                    if (asyncScene.Status == AsyncOperationStatus.Succeeded)
                    {
                        asyncScene.Result.ActivateAsync();
                    }
                    else
                    {
                        throw new Exception($"AssetReference {asyncScene} failed to load.");
                    }
                });
            });
        }
    }
}
