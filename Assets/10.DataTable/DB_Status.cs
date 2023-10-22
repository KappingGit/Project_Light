using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//엑셀 파일 에셋화 스크립트
[ExcelAsset]
public class DB_Status : ScriptableObject
{
	//직렬화 스크립트에서 테이블 데이터(엑셀)의 시트 이름이 동일해야됨...
	//기존 리스트 별 이름 : DB_Status
	// 각 class를 나누어서 리스트를 관리하면 된다...

	public List<DB_StatusEntity> PlayerStatus; // Replace 'EntityType' to an actual type that is serializable.
	public List<DB_StatusEntity_02> MonsterStatus; // Replace 'EntityType' to an actual type that is serializable.
	public List<DB_StatusEntity_03> AttackType; // Replace 'EntityType' to an actual type that is serializable.
}
