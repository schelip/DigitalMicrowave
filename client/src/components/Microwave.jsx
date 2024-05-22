import { useEffect, useRef, useState } from "react";
import DigitalButton from "./DigitalButton";
import HeatingProcedure from "./HeatingProcedure";

export default function Microwave() {
  const statusTextArea = useRef(null);

  const [timeInput, setTimeInput] = useState("");
  const [powerLevelInput, setPowerLevelInput] = useState("10");

  const [errors, setErrors] = useState([]);

  const [time, setTime] = useState("");
  const [powerLevel, setPowerLevel] = useState("");
  const [isRunning, setIsRunning] = useState(false);
  const [isPaused, setIsPaused] = useState(false);
  const [isIdle, setIsIdle] = useState(true);
  const [status, setStatus] = useState(null);

  const [pollingId, setPollingId] = useState(null);

  const [timeInputIsFocused, setTimeInputIsFocused] = useState(false);
  const [powerLevelInputIsFocused, setPowerLevelInputIsFocused] =
    useState(false);
  const [numberButtonsDisabled, setNumberButtonsDisabled] = useState();

  const [heatingProcedures, setHeatingProcedures] = useState([]);
  const [viewHeatingProcedures, setViewHeatingProcedures] = useState(false);
  const [selectedHeatingProcedure, setSelectedHeatingProcedure] =
    useState(null);

  const fetchMicrowave = async () => {
    const response = await fetch(`/api/microwave/get`, {
      method: "GET",
    });
    if (response.status === 200) {
      const microwave = await response.json();
      setTime(microwave.TimeLeft);
      setPowerLevel(microwave.PowerLevel);
      setIsRunning(microwave.CurrentState === "Running");
      setIsPaused(microwave.CurrentState === "Paused");
      setIsIdle(microwave.CurrentState === "Idle");
      setStatus(microwave.Status);
    }
  };

  const fetchHeatingProcedures = async () => {
    const response = await fetch(`/api/heatingProcedure/get`, {
      method: "GET",
    });
    if (response.status === 200) {
      setHeatingProcedures(await response.json());
    }
  };

  useEffect(() => {
    fetchMicrowave();
    fetchHeatingProcedures();
  }, []);

  useEffect(() => {
    (async () => {
      if (isRunning && pollingId === null) {
        const id = setInterval(fetchMicrowave, 1000);
        setPollingId(id);
      } else if (!isRunning && pollingId !== null) {
        clearInterval(pollingId);
        setPollingId(null);
      }
    })();
  }, [isRunning, pollingId]);

  useEffect(() => {
    setNumberButtonsDisabled(
      (!timeInputIsFocused && !powerLevelInputIsFocused) || isRunning
    );
  }, [timeInputIsFocused, powerLevelInputIsFocused, isRunning]);

  useEffect(() => {
    if (statusTextArea.current)
      statusTextArea.current.scrollTop = statusTextArea.current.scrollHeight;
  }, [status]);

  const handleNumberButtonClick = (number) => {
    if (timeInputIsFocused) {
      setTimeInput(timeInput.concat(number));
    } else if (powerLevelInputIsFocused) {
      setPowerLevelInput(powerLevelInput.concat(number));
    }
  };

  const handleStart = async () => {
    const newErrors = [];

    const body = {};
    if (timeInput) body["Time"] = Number(timeInput);
    if (powerLevelInput) body["PowerLevel"] = Number(powerLevelInput);

    const response = await fetch(`/api/microwave/start`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: isRunning ? null : JSON.stringify(body),
    });
    if (response.status === 400) {
      const data = await response.json();
      if (data.ModelState) {
        newErrors.push(Object.values(data.ModelState));
      }
    }
    if (response.status === 500) {
      const data = await response.json();
      newErrors.push(data.ExceptionMessage);
    }
    setErrors(newErrors);
    await fetchMicrowave();
  };

  const handleStop = async () => {
    setErrors([]);
    setTimeInput("");
    setPowerLevelInput("");
    await fetch(`/api/microwave/stop`, {
      method: "POST",
    });
    await fetchMicrowave();
  };

  const handleStartHeatingProcedure = async (id) => {
    setErrors([]);
    setTimeInput("");
    setPowerLevelInput("");
    setViewHeatingProcedures(false);
    await fetch(`/api/microwave/start-proc/${id}`, {
      method: "POST",
    });
    await fetchMicrowave();
  };

  const getForm = () => {
    return (
      <div className="flex flex-col w-full px-2">
        <label className="text-left text-xs" htmlFor="time">
          Tempo (seg)
        </label>
        <input
          id="time"
          type="number"
          className="text-right text-3xl bg-black border-green-500 border-2 w-full"
          value={timeInput}
          onChange={(e) => setTimeInput(e.target.value)}
          onFocus={() => setTimeInputIsFocused(true)}
          onBlur={() => setTimeInputIsFocused(false)}
          max={120}
          min={1}
          autoFocus
        ></input>
        <label className="mt-4 text-left text-xs bg-black" htmlFor="powerLevel">
          Potencia
        </label>
        <input
          id="powerLevel"
          type="number"
          className="text-right text-3xl bg-black border-green-500 border-2 w-full"
          value={powerLevelInput}
          onChange={(e) => setPowerLevelInput(e.target.value)}
          onFocus={() => setPowerLevelInputIsFocused(true)}
          onBlur={() => setPowerLevelInputIsFocused(false)}
          max={10}
          min={1}
        ></input>
      </div>
    );
  };

  const getDisplay = () => {
    return (
      <div className="text-center flex flex-col w-full space-y-2 px-2">
        <div
          className={`text-xs mt-auto ${isPaused ? "visible" : "invisible"}`}
        >
          Pausado
        </div>
        <div className="flex mx-auto flex-row space-x-2">
          <div className="text-3xl">{time}</div>
        </div>
        <div className="text-xs mt-auto">Potencia: {powerLevel}</div>
        <textarea
          className="text-center font-mono text-md bg-black"
          style={{ overflow: "hidden", resize: "none" }}
          value={status}
          rows={2}
          cols={30}
          disabled
          ref={statusTextArea}
        ></textarea>
      </div>
    );
  };

  const getHeatingProceduresList = () => {
    return selectedHeatingProcedure ? (
      <HeatingProcedure
        id={selectedHeatingProcedure}
        onStart={() => handleStartHeatingProcedure(selectedHeatingProcedure)}
        onBack={() => setSelectedHeatingProcedure(null)}
      />
    ) : (
      <div className="flex flex-col relative space-y-2 h-full">
        {heatingProcedures.map((hp) => (
          <button
            className="border-green-500 hover:bg-green-500 hover:text-black border-2"
            key={hp.Id}
            onClick={() => setSelectedHeatingProcedure(hp.Id)}
          >
            {`${hp.Id} - ${hp.Name}`}
          </button>
        ))}
        <button
          className="absolute bottom-0 inset-x-0 border-green-500 hover:bg-green-500 hover:text-black border-2"
          onClick={() => setViewHeatingProcedures(false)}
        >
          Voltar
        </button>
      </div>
    );
  };

  return (
    <div className="flex flex-row space-x-0 m-auto w-11/12 xl:w-5/6 h-5/6 bg-zinc-500">
      <div className="flex relative w-2/3">
        {isRunning}
        <div
          className={`flex w-full my-10 ml-10 bg-[radial-gradient(ellipse_at_center,_var(--tw-gradient-stops))] ${
            isRunning ? "from-yellow-100" : "from-zinc-700"
          } from-0% to-zinc-900 to-100%`}
        ></div>
        <div className="h-[92%] w-1/12 absolute right-8 top-[50%] -translate-y-1/2 rounded-xl bg-gradient-to-r from-zinc-500 from-20% to-zinc-700 to-98%"></div>
      </div>
      <div className="flex w-1/3 border-l-8 border-black p-8">
        <div className="flex flex-col py-4 space-y-4 bg-black font-dseg14 text-green-500 font-bold w-full h-full p-4 pt-8 relative">
          {viewHeatingProcedures ? (
            getHeatingProceduresList()
          ) : (
            <form className="w-full">
              {isIdle ? getForm() : getDisplay()}
              <div className="absolute bottom-5 inset-x-0 w-7/12 mx-auto">
                {errors.length > 0 && (
                  <div className="font-mono text-red-300 text-xs mb-2">
                    Erro:
                    {errors.map((e, i) => (
                      <div key={i}>{e}</div>
                    ))}
                  </div>
                )}
                {heatingProcedures.length !== 0 && isIdle && (
                  <button
                    className="mx-auto mb-4 text-[8pt] border-green-500 hover:bg-green-500 hover:text-black border-2"
                    onClick={() => setViewHeatingProcedures(true)}
                  >
                    Programas de aquecimento
                  </button>
                )}
                <div className="grid grid-cols-3 gap-4 ">
                  {Array(9)
                    .fill()
                    .map((_, i) => {
                      const number = i + 1;
                      return (
                        <DigitalButton
                          key={number}
                          number={i + 1}
                          disabled={numberButtonsDisabled}
                          onClick={() => handleNumberButtonClick(number)}
                        >
                          {number}
                        </DigitalButton>
                      );
                    })}
                  <DigitalButton onClick={handleStop}>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth="3"
                      stroke="currentColor"
                      className="w-6 h-6"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M15.75 5.25v13.5m-7.5-13.5v13.5"
                      />
                    </svg>
                  </DigitalButton>
                  <DigitalButton
                    disabled={numberButtonsDisabled}
                    onClick={() => handleNumberButtonClick(0)}
                  >
                    {0}
                  </DigitalButton>
                  <DigitalButton onClick={handleStart}>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth="3"
                      stroke="currentColor"
                      className="w-6 h-6"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M5.25 5.653c0-.856.917-1.398 1.667-.986l11.54 6.347a1.125 1.125 0 0 1 0 1.972l-11.54 6.347a1.125 1.125 0 0 1-1.667-.986V5.653Z"
                      />
                    </svg>
                  </DigitalButton>
                </div>
              </div>
            </form>
          )}
        </div>
      </div>
    </div>
  );
}
