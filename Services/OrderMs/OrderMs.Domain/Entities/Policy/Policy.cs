using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class Policy : AggregateRoot
    {
        public PolicyId Id { get; private set; }
        public PolicyCoverage Coverage { get; private set; }
        public PolicyIssueDate IssueDate { get; private set; }
        public PolicyExpirationDate ExpirationDate { get; private set; }
        public InsuredVehicleId InsuredVehicleId { get; private set; }
        public FeeId FeeId { get; private set; }
        public InsuredVehicle? InsuredVehicle { get; private set; }
        public Fee? Fee { get; private set; }

        public Policy(PolicyId id, PolicyCoverage coverage, PolicyIssueDate issueDate, PolicyExpirationDate expirationDate, InsuredVehicleId insuredVehicleId, FeeId feeId, InsuredVehicle? insuredVehicle = null, Fee? fee = null)
        {
            Id = id;
            Coverage = coverage;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            InsuredVehicleId = insuredVehicleId;
            FeeId = feeId;
            InsuredVehicle = insuredVehicle;
            Fee = fee;
        }

        public Policy() { }

        public static Policy Update(Policy Policy, PolicyCoverage? coverage, PolicyIssueDate? issueDate, PolicyExpirationDate? expirationDate, InsuredVehicleId? insuredVehicleId, FeeId? feeId)
        {
            var updates = new List<Action>{
                    () => { if (coverage != null) Policy.Coverage = coverage; },
                    () => { if (issueDate != null) Policy.IssueDate = issueDate; },
                    () => { if (expirationDate != null) Policy.ExpirationDate = expirationDate; },
                    () => { if (insuredVehicleId != null) Policy.InsuredVehicleId = insuredVehicleId; },
                    () => { if (feeId != null) Policy.FeeId = feeId; }
                };

            updates.ForEach(update => update());
            return Policy;
        }
    }
}