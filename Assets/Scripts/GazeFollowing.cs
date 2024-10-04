using UnityEngine;

public class CenterObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // ��������3D�I�u�W�F�N�g�̃v���n�u
    public float spawnDistance = 2.0f; // �J��������̋���
    public float horizontalOffset = 0.5f; // ���E�̃I�t�Z�b�g
    public float verticalOffset = 0.0f; // �㉺�̃I�t�Z�b�g

    private Camera mainCamera; // OVRCameraRig�̃J�������i�[����ϐ�
    private GameObject spawnedObject; // �������ꂽ�I�u�W�F�N�g���i�[����ϐ�
    private bool isFollowing = false; // �I�u�W�F�N�g���Ǐ]���邩�ǂ������Ǘ�����t���O

    void Start()
    {
        // OVRCameraRig�̃��C���J�������擾
        mainCamera = Camera.main;
    }

    void Update()
    {
        // �X�y�[�X�L�[���������тɒǏ]ON/OFF��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFollowing = !isFollowing; // �Ǐ]��ON/OFF��؂�ւ���

            if (isFollowing && spawnedObject == null)
            {
                // �Ǐ]��ON�ł��I�u�W�F�N�g���܂���������Ă��Ȃ��ꍇ�ɐ���
                SpawnObjectInCenter();
            }
        }

        // �Ǐ]��ON�̏ꍇ�A�I�u�W�F�N�g���J�����̎����ɒǏ]
        if (isFollowing && spawnedObject != null)
        {
            UpdateObjectPosition();
        }
    }

    void SpawnObjectInCenter()
    {
        // �J�����̑O���ɃI�u�W�F�N�g��z�u���A�I�t�Z�b�g��K�p
        Vector3 spawnPosition = mainCamera.transform.position +
                                mainCamera.transform.forward * spawnDistance +
                                new Vector3(horizontalOffset, verticalOffset, 0);
        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }

    void UpdateObjectPosition()
    {
        // �J�����̑O���ɃI�u�W�F�N�g��Ǐ]���A�I�t�Z�b�g��K�p
        Vector3 updatedPosition = mainCamera.transform.position +
                                  mainCamera.transform.forward * spawnDistance +
                                  new Vector3(horizontalOffset, verticalOffset, 0);
        spawnedObject.transform.position = updatedPosition;
    }
}
