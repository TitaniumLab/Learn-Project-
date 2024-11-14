using System;
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
        [SerializeField] private float _sceneLoadDelay = 0.5f;
        private AsyncOperationHandle<SceneInstance> _loadHangle;
        public static event Action<float> OnSceneChangeWithDelay;

        private void Awake()
        {
            for (int i = 0; i < _lessonsBundleData.Length; i++)
            {
                SetLessonTile(_lessonsBundleData[i]);
            }
        }


        private void SetLessonTile(DataBundleForLessonTileSO lessonTileData)
        {
            var tile = Instantiate(_lessonTilePrefab, _tilesLayout);
            tile.TileImage.sprite = lessonTileData.Sprite;
            tile.TileText.text = lessonTileData.LessonName;
            tile.TileButton.onClick.AddListener(async () =>
            {
                OnSceneChangeWithDelay?.Invoke(_sceneLoadDelay);
                await Awaitable.WaitForSecondsAsync(_sceneLoadDelay);
                _loadHangle = Addressables.LoadSceneAsync(lessonTileData.Scene, LoadSceneMode.Single, false);

                _loadHangle.Completed += ((AsyncOperationHandle<SceneInstance> asyncScene) =>
                {
                    if (asyncScene.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadHangle.Release();
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
