using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

namespace ota.ndi
{
    public class MusicController : MonoBehaviour
    {
        public AudioSource audioSource;  //�C���X�y�N�^�� AudioSource ���Z�b�g
        //public string path = "C:\\otavjtmpmusic\\Protection.wav";
        public float volume = 0;
        public TMP_InputField  musicpathtext;

        private float[] _waveData = new float[1024];

        //�O������̌Ăяo���p���\�b�h
        public void StartAudio()
        {
            if (musicpathtext.text == null)
            {
                Debug.Log("File name is Null!!");
                return;
            }
            string path = musicpathtext.text;
            if (Path.GetExtension(path) == ".m4a")  //��"m4a"�͍Đ��ł��Ȃ����ۂ�
            {
                Debug.Log("Not supported audio format.");
                return;
            }

            StartCoroutine(LoadToAudioClipAndPlay(path));
        }

        //�t�@�C���̓ǂݍ��݁i�_�E�����[�h�j�ƍĐ�
        IEnumerator LoadToAudioClipAndPlay(string path)
        {
            if (audioSource == null || string.IsNullOrEmpty(path))
                yield break;

            if (!File.Exists(path))
            {
                //�����Ƀt�@�C����������Ȃ�����
                Debug.Log("File not found.");
                yield break;
            }

            using (WWW www = new WWW("file://" + path))  //�������܂Ń��[�J���t�@�C���Ƃ���
            {
                while (!www.isDone)
                    yield return null;

                AudioClip audioClip = www.GetAudioClip(false, true);
                if (audioClip.loadState != AudioDataLoadState.Loaded)
                {
                    //�����Ƀ��[�h���s����
                    Debug.Log("Failed to load AudioClip.");
                    yield break;
                }

                //�����Ƀ��[�h��������
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        public void StopAudio()
        {
            audioSource.Stop();
        }

        void Update()
        {
            audioSource.GetOutputData(_waveData, 1);
            volume = _waveData.Select(x => x * x).Sum() / _waveData.Length;
            //Debug.Log(volume);
        }
    }

}
