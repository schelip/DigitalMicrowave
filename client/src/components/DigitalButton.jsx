import PropTypes from "prop-types";

export default function DigitalButton({ disabled, onClick, children }) {
  return (
    <button
      className={`text-center text-lg  border-2 rounded-2xl ${
        disabled
          ? "border-zinc-500 text-zinc-500"
          : "border-green-500 hover:bg-green-500 hover:text-black"
      }`}
      onClick={onClick}
      type="button"
      onMouseDown={(e) => e.preventDefault()}
      disabled={disabled}
    >
      {children}
    </button>
  );
}
DigitalButton.propTypes = {
  disabled: PropTypes.bool,
  onClick: PropTypes.func,
  children: PropTypes.node,
};
