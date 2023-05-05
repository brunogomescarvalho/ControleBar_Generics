using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase<Mesa>
    {
        public int CapacidadeDePessoas { get;private set; }
        public bool Disponivel { get; private set; }


        public Mesa(int capacidadeDePessoas)
        {
            this.Disponivel = true;
            CapacidadeDePessoas = capacidadeDePessoas;
        }

        public void AlterarStatusMesa()
        {
            this.Disponivel = !this.Disponivel;
        }

        public override void AtualizarInformacoes(Mesa mesa)
        {
          
            CapacidadeDePessoas = mesa.CapacidadeDePessoas;
        }

        public override List<string> Validar()
        {
            List<string> list = new();
            if (CapacidadeDePessoas <= 0)
            {
                list.Add("É necessário incluir a quantidade de pessoas que a mesa comporta");
            }
            return list;
        }

        public override string ToString()
        {
            return $"{id,-3} | {CapacidadeDePessoas + " pessoas",-12} | {(Disponivel ? "Disponível" : "Ocupada"),-20}";

        }
    }
}
