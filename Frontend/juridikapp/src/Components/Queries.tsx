import axios from "axios";
import { useState, useEffect } from "react";

const QueryForm = () => {
  const [caseDate, setCaseDate] = useState("");
  const [casePlace, setCasePlace] = useState("");
  const [caseClaimant, setCaseClaimant] = useState("");
  const [caseDefendant, setCaseDefendant] = useState("");
  const [description, setDescription] = useState("");
  const [applicableLaw, setApplicableLaw] = useState("");
  const [applicableJurisprudence, setApplicableJurisprudence] = useState("");
  const [applicableDoctrine, setApplicableDoctrine] = useState("");
  const [query, setQueries] = useState([]);

  useEffect(() => {
    (async () => await Load())();
  }, []);

  async function Load() {
    const result = await axios.get("http://localhost:5283/api/Queries");
    setQueries(result.data);
    console.log(result.data);
  }

  async function save(event: any) {
    event.preventDefault();
    try {
      const payload = {
        CaseDate: caseDate,
        CasePlace: casePlace,
        CaseClaimant: caseClaimant,
        CaseDefendant: caseDefendant,
        Description: description,
        ApplicableLaw: applicableLaw,
        ApplicableJurisprudence: applicableJurisprudence,
        ApplicableDoctrine: applicableDoctrine,
      };
      const response = await axios.post(
        "http://localhost:5283/api/Queries",
        payload
      );
      const { data } = response;
      setCaseDate("");
      setCasePlace("");
      setCaseClaimant("");
      setCaseDefendant("");
      setDescription("");
      setApplicableLaw("");
      setApplicableJurisprudence("");
      setApplicableDoctrine("");
      Load();
    } catch (err) {
      alert(err);
    }
  }

  /*
async function save(event: any) {
    event.preventDefault();
    try {
      await axios.post("http://localhost:5283/api/Queries", {
        CaseDate: caseDate,
        CasePlace: casePlace,
        CaseClaimant: caseClaimant,
        CaseDefendant: caseDefendant,
        Description: description,
        ApplicableLaw: applicableLaw,
        ApplicableJurisprudence: applicableJurisprudence,
        ApplicableDoctrine: applicableDoctrine,
      });
      setCaseDate("");
      setCasePlace("");
      setCaseClaimant("");
      setCaseDefendant("");
      setDescription("");
      setApplicableLaw("");
      setApplicableJurisprudence("");
      setApplicableDoctrine("");
      Load();
    } catch (err) {
      alert(err);
    }
  }*/

  return (
    <div>
      <h1>CASE</h1>
      <div className="container mt-4">
        <form>
          <div className="form-group">
            <label>Case Date</label>
            <input
              type="text"
              className="form-control"
              id="casedate"
              value={caseDate}
              onChange={(event) => {
                setCaseDate(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Case Date</label>
            <input
              type="text"
              className="form-control"
              id="caseplace"
              value={casePlace}
              onChange={(event) => {
                setCasePlace(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Claimant</label>
            <input
              type="text"
              className="form-control"
              id="claimant"
              value={caseClaimant}
              onChange={(event) => {
                setCaseClaimant(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Defendant</label>
            <input
              type="text"
              className="form-control"
              id="defendant"
              value={caseDefendant}
              onChange={(event) => {
                setCaseDefendant(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Description</label>
            <input
              type="text"
              className="form-control"
              id="description"
              value={description}
              onChange={(event) => {
                setDescription(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Applicable Law</label>
            <input
              type="text"
              className="form-control"
              id="applicablelaw"
              value={applicableLaw}
              onChange={(event) => {
                setApplicableLaw(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Applicable Jurisprudence</label>
            <input
              type="text"
              className="form-control"
              id="applicablejurisprudence"
              value={applicableJurisprudence}
              onChange={(event) => {
                setApplicableJurisprudence(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label>Applicable Doctrine</label>
            <input
              type="text"
              className="form-control"
              id="applicabledoctrine"
              value={applicableDoctrine}
              onChange={(event) => {
                setApplicableDoctrine(event.target.value);
              }}
            />
          </div>

          <div>
            <button className="btn btn-primary mt-4" onClick={save}>
              SUBMIT
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default QueryForm;
