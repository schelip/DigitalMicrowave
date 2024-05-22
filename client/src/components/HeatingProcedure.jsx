import PropTypes from "prop-types";
import { useEffect, useState } from "react";

export default function HeatingProcedure({ id, onStart, onBack }) {
  const [name, setName] = useState();
  const [foodName, setFoodName] = useState();
  const [time, setTime] = useState();
  const [powerLevel, setPowerLevel] = useState();
  const [instructions, setInstructions] = useState();

  useEffect(() => {
    (async () => {
      const response = await fetch(`/api/heatingProcedure/get/${id}`, {
        method: "GET",
      });

      if (response.status === 200) {
        const data = await response.json();
        setName(data.Name);
        setFoodName(data.FoodName);
        setTime(data.Time);
        setPowerLevel(data.PowerLevel);
        setInstructions(data.Instructions);
      }
    })();
  }, [id]);

  return (
    <div className="flex flex-col space-y-2 h-full">
      <div className="border-b-2 pb-2 border-green-500">{`${id} - ${name}`}</div>
      <div className="text-xs">{`${foodName}`}</div>
      <div className="text-xs pt-2">{`Tempo: ${time} seg`}</div>
      <div className="text-xs">{`Potencia: ${powerLevel}`}</div>
      <div className="text-xs pt-4">{`Instrucoes:`}</div>
      <div className="text-xs grow font-mono">{`${instructions}`}</div>
      <button
        className="border-green-500 bg-green-500 hover:bg-black text-black hover:text-green-500 border-2"
        onClick={onStart}
      >
        Selecionar
      </button>
      <button
        className="border-green-500 hover:bg-green-500 hover:text-black border-2"
        onClick={onBack}
      >
        Voltar
      </button>
    </div>
  );
}
HeatingProcedure.propTypes = {
  id: PropTypes.number,
};
