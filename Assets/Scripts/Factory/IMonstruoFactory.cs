using UnityEngine;
using FearPark.Data;
using FearPark.Enemies;

namespace FearPark.Factory
{
    public interface IMonstruoFactory
    {
        MonstruoBase CrearMonstruo(MonsterDataSO data, Vector3 posicion);
    }
}
