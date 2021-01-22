using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MVCNhibernate.Models
{
    public class Departmanlar
    {
        public virtual int Departman_ID { get; set; }
        public virtual string Departman_Ad { get; set; }
        public virtual string Telefon { get; set; }
        public virtual ICollection<Görevliler> Calisanlar { get; set; } = new List<Görevliler>();
    }
    public class DepartmanlarMap : ClassMapping<Departmanlar>
    {
        public DepartmanlarMap()
        {
            Table("Departmanlar");
            Id(x => x.Departman_ID, m => m.Generator(Generators.Native));
            Property(x => x.Departman_Ad, c => c.Length(30));
            Property(x => x.Telefon, c => c.Length(30));

            Set(e => e.Calisanlar,
                mapper =>
                {
                    mapper.Key(k => k.Column("Departman"));
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.All);
                },
               relation => relation.OneToMany(mapping => mapping.Class(typeof(Görevliler))));
        }
    }
}