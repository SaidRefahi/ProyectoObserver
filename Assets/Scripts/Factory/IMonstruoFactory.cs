using UnityEngine;
using FearPark.Data;
using FearPark.Enemies;

namespace FearPark.Factory
{
    public interface IMonstruoFactory
    {
        // LSP (Liskov Substitution Principle): El cliente que llama a este método no necesita saber 
        // qué monstruo exacto se creó (Lento, Rápido, Élite). Solo le importa que herede de MonstruoBase.
        MonstruoBase CrearMonstruo(MonsterDataSO data, Vector3 posicion);
    }
}
