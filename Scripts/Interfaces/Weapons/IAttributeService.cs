/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 01/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using System;

namespace Weapons
{
    public interface IAttributeService
    {
        Guid Id { get; }

        void Initialize(ProjectileController projectileController);
    }
}