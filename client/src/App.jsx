import Microwave from "./components/Microwave";
import "./App.css";

function App() {
  return (
    <main>
      <div className="flex flex-col space-y-0">
        <div className="flex justify-start bg-zinc-800 h-16">
          <div className="text-white my-auto ml-12">
            <h1 className="font-dseg14 font-bold text-2xl text-green-500">
              Digital Microwave
            </h1>
          </div>
        </div>
        <div className="flex h-full w-full">
          <Microwave />
        </div>
      </div>
    </main>
  );
}

export default App;
