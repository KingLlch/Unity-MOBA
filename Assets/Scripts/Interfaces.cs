public interface IHealthDamageable
{
    public void ApplyHealthDamage(int damage);
}

public interface IHealthHealable
{
    public void ApplyHealthHeal(int heal);
}

public interface IManaHealable
{
    public void ApplyManaHeal(int heal);
}

public interface IManaDamageable
{
    public void ApplyManaDamage(int damage);
}
