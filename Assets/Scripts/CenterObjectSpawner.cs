using UnityEngine;
using OculusSampleFramework; // Oculus Integration��Hand Tracking�p���O��Ԃ�ǉ�

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // ��������I�u�W�F�N�g
    public OVRHand leftHand;        // ����̃n���h�g���b�L���O�����擾���邽�߂�OVRHand
    public float spawnDistance = 0.5f; // �J��������ǂꂾ�����ꂽ�ʒu�ɃI�u�W�F�N�g�𐶐����邩
    private GameObject spawnedObject; // ���������I�u�W�F�N�g��ێ�����

    void Update()
    {
        // ���肪���݂��A�g���b�L���O����Ă���ꍇ
        if (leftHand != null && leftHand.IsTracked)
        {
            // �e�w�Ɛl�����w���܂�ł����Ԃ��擾
            bool isPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            // �I�u�W�F�N�g���܂���������Ă��Ȃ��ꍇ�A�܂񂾂Ƃ��ɐ���
            if (isPinching && spawnedObject == null)
            {
                // �J�����̈ʒu����ɃI�u�W�F�N�g�𐶐�
                Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
                spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }

            // �I�u�W�F�N�g����������Ă���ꍇ�A�J�����̎����ɒǏ]������
            if (spawnedObject != null)
            {
                Vector3 followPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
                spawnedObject.transform.position = followPosition;
            }

            // �w�𗣂�����I�u�W�F�N�g������
            if (!isPinching && spawnedObject != null)
            {
                Destroy(spawnedObject);
                spawnedObject = null; // ���������I�u�W�F�N�g�̎Q�Ƃ����Z�b�g
            }
        }
    }
}
