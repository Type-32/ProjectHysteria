using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = System.Random;

namespace Hysteria.SceneBehaviour
{
    public class SceneLightningController : MonoBehaviour
    {
        [InfoBox("Put your directional light script here.")]
        [SerializeField] Light directionalLight;

        [MinMaxSlider(1, 4), SerializeField] private Vector2 flashCountRange;
        [SerializeField] private float flashDurationIntervalSeconds = 30f;
        [MinMaxSlider(0.01f, 0.1f), SerializeField] private Vector2 flashContinuousSecondsInterval;
        [MinValue(0f), SerializeField] private float targetLightIntensity = 1000f;

        private float _initialLightIntensity;
        private bool _isFlashing = false;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Random random;
        private void Start()
        {
            random = new Random();
            if (directionalLight == null)
            {
                Debug.LogError("Directional Light not assigned to SceneLightningController!");
                enabled = false;
                return;
            }

            _initialLightIntensity = directionalLight.intensity;
            StartCoroutine(LightningRoutine());
        }

        public IEnumerator LightningRoutine()
        {
            yield return new WaitForSeconds(flashDurationIntervalSeconds);

            int flashCount = Mathf.RoundToInt(random.Next((int)flashCountRange.x, (int)flashCountRange.y));
            for (int i = 0; i < flashCount; i++)
            {
                StartCoroutine(FlashLightning());
                yield return new WaitForSeconds(0.05f);
            }

            StartCoroutine(LightningRoutine());
        }

        private IEnumerator FlashLightning()
        {
            _isFlashing = true;
            directionalLight.intensity = targetLightIntensity;

            yield return _waitForFixedUpdate;

            directionalLight.intensity = _initialLightIntensity;
            _isFlashing = false;
        }
        
        private async Task FlashLightningTest(float duration = 0.1f)
        {
            _initialLightIntensity = directionalLight.intensity;

            _isFlashing = true;
            directionalLight.intensity = targetLightIntensity;

            await Task.Delay((int)(duration * 1000));

            directionalLight.intensity = _initialLightIntensity;
            _isFlashing = false;
        }

        private void FixedUpdate()
        {
            if (directionalLight && !_isFlashing)
            {
                // Optionally, you can add some subtle random fluctuations here
                directionalLight.intensity = _initialLightIntensity + random.Next(-5, 5);
            }
        }

        [Button("Trigger Lightning")]
        private async void TriggerLightning()
        {
            await FlashLightningTest();
        }
    }
}